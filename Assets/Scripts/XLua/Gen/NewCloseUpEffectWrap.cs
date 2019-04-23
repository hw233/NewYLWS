﻿#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class NewCloseUpEffectWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(NewCloseUpEffect);
			Utils.BeginObjectRegister(type, L, translator, 0, 1, 5, 5);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DoEffect", _m_DoEffect);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "maskBGColor", _g_get_maskBGColor);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "maskFGColor", _g_get_maskFGColor);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "iterations", _g_get_iterations);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "blurSpread", _g_get_blurSpread);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "downSample", _g_get_downSample);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "maskBGColor", _s_set_maskBGColor);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "maskFGColor", _s_set_maskFGColor);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "iterations", _s_set_iterations);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "blurSpread", _s_set_blurSpread);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "downSample", _s_set_downSample);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 6, 2, 2);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "ApplyCloseUpEffect", _m_ApplyCloseUpEffect_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StopCloseUpEffect", _m_StopCloseUpEffect_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AdjustBGColor", _m_AdjustBGColor_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AdjustBGColorComplete", _m_AdjustBGColorComplete_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetCloseUpEffectColor", _m_SetCloseUpEffectColor_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "MaskBGColor", _g_get_MaskBGColor);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "MaskFGColor", _g_get_MaskFGColor);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "MaskBGColor", _s_set_MaskBGColor);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "MaskFGColor", _s_set_MaskFGColor);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					NewCloseUpEffect gen_ret = new NewCloseUpEffect();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to NewCloseUpEffect constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ApplyCloseUpEffect_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Material _mat = (UnityEngine.Material)translator.GetObject(L, 1, typeof(UnityEngine.Material));
                    UnityEngine.GameObject[] _gos = (UnityEngine.GameObject[])translator.GetObject(L, 2, typeof(UnityEngine.GameObject[]));
                    int _lowLayer = LuaAPI.xlua_tointeger(L, 3);
                    int _highLayer = LuaAPI.xlua_tointeger(L, 4);
                    int _isPrepareDazhao = LuaAPI.xlua_tointeger(L, 5);
                    UnityEngine.Material _blurMat = (UnityEngine.Material)translator.GetObject(L, 6, typeof(UnityEngine.Material));
                    
                    NewCloseUpEffect.ApplyCloseUpEffect( _mat, _gos, _lowLayer, _highLayer, _isPrepareDazhao, _blurMat );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopCloseUpEffect_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    NewCloseUpEffect.StopCloseUpEffect(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoEffect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NewCloseUpEffect gen_to_be_invoked = (NewCloseUpEffect)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.DoEffect(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AdjustBGColor_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Camera>(L, 1)&& translator.Assignable<UnityEngine.Color>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 1, typeof(UnityEngine.Camera));
                    UnityEngine.Color _color;translator.Get(L, 2, out _color);
                    float _time = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    NewCloseUpEffect.AdjustBGColor( _camera, _color, _time );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Camera>(L, 1)&& translator.Assignable<UnityEngine.Color>(L, 2)) 
                {
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 1, typeof(UnityEngine.Camera));
                    UnityEngine.Color _color;translator.Get(L, 2, out _color);
                    
                    NewCloseUpEffect.AdjustBGColor( _camera, _color );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to NewCloseUpEffect.AdjustBGColor!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AdjustBGColorComplete_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    NewCloseUpEffect.AdjustBGColorComplete(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCloseUpEffectColor_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Color _color;translator.Get(L, 1, out _color);
                    
                    NewCloseUpEffect.SetCloseUpEffectColor( _color );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MaskBGColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushUnityEngineColor(L, NewCloseUpEffect.MaskBGColor);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MaskFGColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushUnityEngineColor(L, NewCloseUpEffect.MaskFGColor);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_maskBGColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NewCloseUpEffect gen_to_be_invoked = (NewCloseUpEffect)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineColor(L, gen_to_be_invoked.maskBGColor);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_maskFGColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NewCloseUpEffect gen_to_be_invoked = (NewCloseUpEffect)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineColor(L, gen_to_be_invoked.maskFGColor);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_iterations(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NewCloseUpEffect gen_to_be_invoked = (NewCloseUpEffect)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.iterations);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_blurSpread(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NewCloseUpEffect gen_to_be_invoked = (NewCloseUpEffect)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.blurSpread);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_downSample(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NewCloseUpEffect gen_to_be_invoked = (NewCloseUpEffect)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.downSample);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MaskBGColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			UnityEngine.Color gen_value;translator.Get(L, 1, out gen_value);
				NewCloseUpEffect.MaskBGColor = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MaskFGColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			UnityEngine.Color gen_value;translator.Get(L, 1, out gen_value);
				NewCloseUpEffect.MaskFGColor = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_maskBGColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NewCloseUpEffect gen_to_be_invoked = (NewCloseUpEffect)translator.FastGetCSObj(L, 1);
                UnityEngine.Color gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.maskBGColor = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_maskFGColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NewCloseUpEffect gen_to_be_invoked = (NewCloseUpEffect)translator.FastGetCSObj(L, 1);
                UnityEngine.Color gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.maskFGColor = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_iterations(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NewCloseUpEffect gen_to_be_invoked = (NewCloseUpEffect)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.iterations = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_blurSpread(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NewCloseUpEffect gen_to_be_invoked = (NewCloseUpEffect)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.blurSpread = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_downSample(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NewCloseUpEffect gen_to_be_invoked = (NewCloseUpEffect)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.downSample = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
