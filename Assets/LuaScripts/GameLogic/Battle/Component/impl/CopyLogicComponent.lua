local table_insert = table.insert
local table_remove = table.remove
local Utils = Utils
local Vector3 = Vector3
local Quaternion = Quaternion
local GameUtility = CS.GameUtility

local BaseBattleLogicComponent = require "GameLogic.Battle.Component.BaseBattleLogicComponent"
local CopyLogicComponent = BaseClass("CopyLogicComponent", BaseBattleLogicComponent)
  
local BaoxiangPath = "Effect/Prefab/Battle/baoxiang.prefab"

function CopyLogicComponent:__init(copyLogic)
    self.m_dropList = {}        -- {item_id=, count=}
    self.m_dropDic = {}         -- dropIndex --> {item_id=, count=}
    self.m_dropIndex = 0
    self.m_boxList = {}         -- {gameobjs, pos, item_id} []
    self.m_logic = copyLogic

    HallConnector:GetInstance():RegisterHandler(MsgIDDefine.COPY_RSP_FINISH_COPY, Bind(self, self.RspBattleFinish))
end

function CopyLogicComponent:__delete()
    HallConnector:GetInstance():ClearHandler(MsgIDDefine.COPY_RSP_FINISH_COPY)

    self.m_dropList = nil
    self.m_dropDic = nil
    self.m_logic = nil

    for _, v in ipairs(self.m_boxList) do
        local go, pos, item_id = v[1], v[2], v[3]
        UIManagerInst:Broadcast(UIMessageNames.UIBATTLE_PICK_BOX, pos, item_id)
        GameObjectPoolInst:RecycleGameObject(BaoxiangPath, go)        
    end
    self.m_boxList = nil
end
  
function CopyLogicComponent:ShowBattleUI()
    UIManagerInst:OpenWindow(UIWindowNames.UIBattleMain)
    BaseBattleLogicComponent.ShowBloodUI(self)
end

function CopyLogicComponent:ReqBattleFinish(copyID, playerWin, take_time, star)
  --todo 使用假数据做rsp响应
--   local battleAwardData = {
--     finish_result = playerWin and 0 or 1,
--   }

--   self.m_logic:OnAward(battleAwardData)
 
   --[[   if isWin then
    else
    end ]]

    local msg_id = MsgIDDefine.COPY_REQ_FINISH_COPY
    local msg = (MsgIDMap[msg_id])()
    if playerWin then
        msg.finish_result = 0
    else
        msg.finish_result = 1
    end
    msg.take_time = take_time
    msg.pass_star = star
    msg.copy_id = copyID
    -- local frameCmdList = CtlBattleInst:GetFrameCmdList()
    -- PBUtil.ConvertCmdListToProto(msg.battle_info.cmd_list, frameCmdList)
    -- self:GenerateResultInfoProto(msg.battle_result)
	HallConnector:GetInstance():SendMessage(msg_id, msg)
end

function CopyLogicComponent:RspBattleFinish(msg_obj)
    -- Logger.Log('CopyLogicComponent msg_obj: ' .. tostring(msg_obj))

	local result = msg_obj.result
	if result ~= 0 then
		Logger.LogError('CopyLogicComponent failed: '.. result)
		return
    end
    
    local finish_result = msg_obj.finish_result

    UIManagerInst:CloseWindow(UIWindowNames.UIBattleMain)
    UIManagerInst:OpenWindow(UIWindowNames.BattleSettlement, msg_obj, true)
end

function CopyLogicComponent:CacheDropList(list1, list2)
    if list1 then
        for _, v in ipairs(list1) do
            table_insert(self.m_dropList, {item_id = v.item_id, count = v.count})
        end
    end

    if list2 then
        for _, v in ipairs(list2) do
            table_insert(self.m_dropList, {item_id = v.item_id, count = v.count})
        end
    end
end

function CopyLogicComponent:DistributeDrop(monsterCount)
    if monsterCount <= 1 then
        return
    end

    local tmp = {}
    for i = 1, monsterCount do
        table_insert(tmp, i)
    end

    for _, v in ipairs(self.m_dropList) do
        local count = #tmp
        local idx = Utils.RandomBetween(1, count)

        self.m_dropDic[tmp[idx]] = v
        table_remove(tmp, idx)
    end
end

function CopyLogicComponent:MonsterDrop(actor)
    self.m_dropIndex = self.m_dropIndex + 1

    local one_item = self.m_dropDic[self.m_dropIndex]
    if one_item then
        local x,y,z = actor:GetPosition():GetXYZ()
        local diePos = Vector3.New(x, y, z)

        GameObjectPoolInst:GetGameObjectAsync(BaoxiangPath,
            function(inst)
                inst.transform:SetPositionAndRotation(diePos, Quaternion.identity)            

                GameUtility.SetShadowHeight(inst, inst.transform.position.y, 0.06)
                table_insert(self.m_boxList, {inst, diePos, one_item.item_id})   
            end)
    end
end

function CopyLogicComponent:AutoPick()
    for _, v in ipairs(self.m_boxList) do
        local go, pos, item_id = v[1], v[2], v[3]
        UIManagerInst:Broadcast(UIMessageNames.UIBATTLE_PICK_BOX, pos, item_id)
        GameObjectPoolInst:RecycleGameObject(BaoxiangPath, go)        
    end

    self.m_boxList = {}
end

function CopyLogicComponent:HideBox()
    for _, v in ipairs(self.m_boxList) do
        local go, pos, item_id = v[1], v[2], v[3]
        go.transform.localPosition = Vector3.New(pos.x + 999, pos.y, pos.z)
    end
end

function CopyLogicComponent:ShowBox()
    for _, v in ipairs(self.m_boxList) do
        local go, pos, item_id = v[1], v[2], v[3]
        go.transform.localPosition = pos
    end
end

function CopyLogicComponent:NeedPlot(copyID)
    if not Player:GetInstance():GetMainlineMgr():IsCopyClear(copyID) then
        return true
    end
    if Player:GetInstance():GetLineupMgr():IsEnbalePlotMode() then
        return true
    end
    return false
end

return CopyLogicComponent