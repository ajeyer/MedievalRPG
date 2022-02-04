public enum AnimationName
{
    idleDown,
    idleUp,
    idleRight,
    idleLeft,
    walkDown,
    walkUp,
    walkRight,
    walkLeft,
    runDown,
    runUp,
    runRight,
    runLeft,
    useToolDown,
    useToolUp,
    useToolRight,
    useToolLeft,
    swingToolDown,
    swingToolUp,
    swingToolRight,
    swingToolLeft,
    liftToolDown,
    liftToolUp,
    liftToolRight,
    liftToolLeft,
    holdToolDown,
    holdToolUp,
    holdToolRight,
    holdToolLeft,
    pickDown,
    pickUp,
    pickRight,
    pickLeft,
    count
}

public enum CharacterPartAnimator
{
    body,
    arms,
    hair,
    tool,
    hat,
    count
}

public enum PartVariantColor
{
    none,
    count
}

public enum PartVariantType
{
    none,
    carry,
    hoe,
    pickaxe,
    axe,
    scythe,
    wateringCan,
    count
}

public enum GridBoolProperty
{
    diggable,
    canDropItem,
    canPlaceFurniture,
    isPath, //used later for NPC
    isNPCObstacle //used later for NPC
}

public enum InventoryLocation
{
    player,
    chest,
    count
}

public enum SceneName 
{
    Scene1_Farm,
    Scene2_Field,
    Scene3_Cabin,
    Scene4_SarahsHouse,
    Scene5_JohnnisHouse,
    Scene6_MyHouse
}

public enum Season
{
    Spring,
    Summer,
    Fall,
    Winter,
    none,
    count
}

public enum ToolEffect
{
    none,
    watering
}

public enum HarvestActionEffect
{
    leavesFalling,
    pineConesFalling,
    choppingTreeTrunk,
    breakingStone,
    reaping,
    none
}

public enum Direction
{
    up, 
    down,
    left,
    right,
    none
}

public enum ItemType
{
    Seed,
    Commodity,
    Watering_tool,
    Hoeing_tool,
    Chopping_tool,
    Breaking_tool,
    Reaping_tool,
    Collecting_tool,
    Reapable_scenary,
    furniture,
    none,
    count

}

