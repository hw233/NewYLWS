local BattleEnum = BattleEnum
local StatusGiver = StatusGiver
local StatusFactoryInst = StatusFactoryInst
local FixMul = FixMath.mul
local FixDiv = FixMath.div
local FixSub = FixMath.sub
local FixAdd = FixMath.add
local CtlBattleInst = CtlBattleInst
local ActorManagerInst = ActorManagerInst
local FixIntMul = FixMath.muli
local IsInCircle = SkillRangeHelper.IsInCircle
local FixNewVector3 = FixMath.NewFixVector3
local FixNormalize = FixMath.Vector3Normalize
local Formular = Formular
local StatusEnum = StatusEnum
local FixAbs = FixMath.abs

local SkillBase = require "GameLogic.Battle.Skill.SkillBase"
local Skill10171 = BaseClass("Skill10171", SkillBase)

function Skill10171:Perform(performer, target, performPos, special_param)
    if not performer or not performer:IsLive() then
        return
    end

    -- 1
    -- 典韦对当前攻击目标及其周围半径{A}米的敌人发起嘲讽，吸引其主动攻击自身，同时进入绝对防御状态，受到所有伤害降低{x1}%，持续{B}秒。
    -- 状态持续期间典韦无法移动和攻击，并免疫浮空和击飞效果。
    -- 2-3
    -- 典韦对当前攻击目标及其周围半径{A}米的敌人发起嘲讽，吸引其主动攻击自身，同时进入绝对防御状态，受到所有伤害降低{x2}%，持续{B}秒。
    -- 状态持续期间典韦无法移动和攻击，并免疫浮空和击飞效果。在绝对铁壁结束时，典韦立即冲向当前攻击目标，对目标及其周围敌人发起一次旋转攻击，造成{y2}（+{E}%物攻)点物理伤害。
    -- 4-6
    -- 典韦对当前攻击目标及其周围半径{A}米的敌人发起嘲讽，吸引其主动攻击自身，同时进入绝对防御状态，受到所有伤害降低{x4}%，持续{B}秒。
    -- 状态持续期间典韦无法移动和攻击，并免疫浮空和击飞效果。在绝对铁壁结束时，典韦立即冲向当前攻击目标，对目标及其周围敌人发起一次旋转攻击，造成{y4}（+{E}%物攻)点物理伤害。
    -- 此次攻击每命中一个敌人，典韦就获得额外{C}层铁之藩篱。

    
    local battleLogic = CtlBattleInst:GetLogic()
    local factory = StatusFactoryInst
    local time = FixIntMul(self:B(), 1000)
    local StatusGiverNew = StatusGiver.New

    if special_param.keyFrameTimes == 1 then -- 嘲讽阶段
        if not target or not target:IsLive() then
            return
        end

        local radius = self:A()
        local targetPos = target:GetPosition()
        local performDir = FixNormalize(performPos - performer:GetPosition())
        ActorManagerInst:Walk(
            function(tmpTarget)
                if not battleLogic:IsEnemy(performer, tmpTarget, BattleEnum.RelationReason_SKILL_RANGE) then
                    return
                end

                if not self:InRange(performer, tmpTarget, performDir, nil) then
                    return
                end

                local giver = StatusGiverNew(performer:GetActorID(), 10171)
                local statusChaofeng = factory:NewStatusChaoFeng(giver, performer:GetActorID(), time)
                self:AddStatus(performer, tmpTarget, statusChaofeng)
            end
        )

    elseif special_param.keyFrameTimes == 2 then -- 加伤害减免阶段
        local giver = StatusGiver.New(performer:GetActorID(), 10171)
        local immuneBuff = StatusFactoryInst:NewStatusImmune(giver, time)
        immuneBuff:AddImmune(StatusEnum.IMMUNEFLAG_HURTFLY)
        immuneBuff:SetCanClearByOther(false)
        self:AddStatus(performer, performer, immuneBuff)

        local giver = StatusGiver.New(performer:GetActorID(), 10171) 
        local statusNTimeBeHurtChg = StatusFactoryInst:NewStatusNTimeBeHurtMul(giver, time, FixSub(1, FixDiv(self:X(), 100)), {21016})
        statusNTimeBeHurtChg:AddBeHurtMulType(BattleEnum.HURTTYPE_PHY_HURT)
        statusNTimeBeHurtChg:AddBeHurtMulType(BattleEnum.HURTTYPE_MAGIC_HURT)
        statusNTimeBeHurtChg:AddBeHurtMulType(BattleEnum.HURTTYPE_REAL_HURT)
        self:AddStatus(performer, performer, statusNTimeBeHurtChg)
        performer:AddEffect(101706)

    elseif special_param.keyFrameTimes == 3 then -- 技能等级 1 阶段结束  不做任何处理，仅用来判断是否继续第二阶段

    elseif special_param.keyFrameTimes == 4 then -- 2 阶段 位移
        if not target or not target:IsLive() then
            return
        end

        local movehelper = performer:GetMoveHelper()
        if movehelper then
            local performerPos = performer:GetPosition()
            local targetPos = target:GetPosition()
            local dir1 = targetPos - performerPos
            local distance = dir1:Magnitude()
            local dir = FixNormalize(dir1)
            local dis = FixAbs(FixSub(distance, target:GetRadius()))
            dir:Mul(dis)
            dir:Add(performerPos)

            local movePos = dir
            local pathHandler = CtlBattleInst:GetPathHandler()
            if pathHandler then
                local x,y,z = performerPos:GetXYZ()
                local x2, y2, z2 = movePos:GetXYZ()
                local hitPos = pathHandler:HitTest(x, y, z, x2, y2, z2)
                if hitPos then
                    movePos:SetXYZ(hitPos.x , performerPos.y, hitPos.z)
                end
            end

            local distance = (movePos - performerPos):Magnitude()
            local time1 = 0.4
            local speed = FixDiv(distance, time1)
            movehelper:Stop()
            movehelper:Start({ movePos }, speed, nil, true)
        end
        
    elseif special_param.keyFrameTimes == 5 then -- 2 阶段 攻击
        if not target or not target:IsLive() then
            return
        end

        local radius = self.m_skillCfg.dis2
        local targetPos = target:GetPosition()
        local atkCount = 0
        ActorManagerInst:Walk(
            function(tmpTarget)
                if not battleLogic:IsEnemy(performer, tmpTarget, BattleEnum.RelationReason_SKILL_RANGE) then
                    return
                end

                if not IsInCircle(targetPos, radius, tmpTarget:GetPosition(), tmpTarget:GetRadius()) then
                    return
                end

                local judge = Formular.AtkRoundJudge(performer, tmpTarget, BattleEnum.HURTTYPE_PHY_HURT, true)
                if Formular.IsJudgeEnd(judge) then
                    return  
                end

                if self.m_level >= 4 then
                    atkCount = FixAdd(atkCount, 1)
                end

                local injure = Formular.CalcInjure(performer, tmpTarget, self.m_skillCfg, BattleEnum.HURTTYPE_PHY_HURT, judge, self:Y())
                if injure > 0 then
                    local giver = StatusGiverNew(performer:GetActorID(), 10171)
                    local status = factory:NewStatusHP(giver, FixMul(-1, injure), BattleEnum.HURTTYPE_PHY_HURT, BattleEnum.HPCHGREASON_BY_SKILL, 
                                                                                                                                judge, special_param.keyFrameTimes)
                    self:AddStatus(performer, tmpTarget, status)
                end
            end
        )

        if self.m_level >= 4 and atkCount > 0 then
            local addCount = FixMul(atkCount, self:C())
            performer:Add10173Count(addCount)
        end
    end
end

return Skill10171