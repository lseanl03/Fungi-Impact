﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{

    //Kích hoạt khi chọn Fungus ra trận
    public delegate void OnPickFungus(int slotIndex, FungusPackedConfig config);
    public static event OnPickFungus onPickFungus;
    public static void ActionOnPickFungus(int slotIndex, FungusPackedConfig config)
    {
        onPickFungus?.Invoke(slotIndex, config);
    }

    //Kích hoạt khi bỏ chọn Fungus đang chọn để ra trận
    public delegate void OnUnPickFungus(int slotIndex, FungusPackedConfig config);
    public static event OnUnPickFungus onUnPickFungus;
    public static void ActionOnUnPickFungus(int slotIndex, FungusPackedConfig config)
    {
        onUnPickFungus?.Invoke(slotIndex, config);
    }

    //Kích hoạt khi chuyển đổi Fungus trong chiến đấu
    public delegate void OnSwitchFungus(FungusInfoReader oldFungusInfo, FungusInfoReader newFungusInfo, FungusCurrentStatusHUD fungusCurrentStatusHUD);
    public static event OnSwitchFungus onSwitchFungus;
    public static void ActionOnSwitchFungus(FungusInfoReader oldFungusInfo, FungusInfoReader newFungusInfo, FungusCurrentStatusHUD fungusCurrentStatusHUD)
    {
        onSwitchFungus?.Invoke(oldFungusInfo, newFungusInfo, fungusCurrentStatusHUD);
    }

    //Kích hoạt khi ấn nút xem thông tin boss
    public delegate void OnShowInfoBoss(BossSlot bossSlot, bool state);
    public static event OnShowInfoBoss onShowInfoBoss;
    public static void ActionOnShowInfoBoss(BossSlot bossSlot, bool state)
    {
        onShowInfoBoss?.Invoke(bossSlot, state);
    }

    //Kích hoạt khi chọn Boss để chiến đấu
    public delegate void OnSelectBoss(BossSlot bossSlot);
    public static event OnSelectBoss onSelectBoss;
    public static void ActionOnSelectBoss(BossSlot bossSlot)
    {
        onSelectBoss?.Invoke(bossSlot);
    }

    //Kích hoạt khi Fungus chết
    public delegate void OnFungusDie();
    public static event OnFungusDie onFungusDie;
    public static void ActionOnFungusDie()
    {
        onFungusDie?.Invoke();
    }

    //Kích hoạt khi camera thay đổi mục tiêu
    public delegate void OnCameraChangeTarget(Transform target);
    public static event OnCameraChangeTarget onCameraChangeTarget;
    public static void ActionOnCameraChangeTarget(Transform target)
    {
        onCameraChangeTarget?.Invoke(target);
    }

    //Kích hoạt khi sinh sản Fungus lúc mới vào chiến đấu
    public delegate void OnSpawnFungusInit(List<FungusInfoReader> fungusList);
    public static event OnSpawnFungusInit onSpawnFungusInit;
    public static void ActionOnSpawnFungusInit(List<FungusInfoReader> fungusList)
    {
        onSpawnFungusInit?.Invoke(fungusList);
    }

    //Kích hoạt khi sinh sản Boss lúc mới vào chiến đấu
    public delegate void OnSpawnBossInit(BossInfoReader bossInfo);
    public static event OnSpawnBossInit onSpawnBossInit;
    public static void ActionOnSpawnBossInit(BossInfoReader bossInfo)
    {
        onSpawnBossInit?.Invoke(bossInfo);
    }
}
