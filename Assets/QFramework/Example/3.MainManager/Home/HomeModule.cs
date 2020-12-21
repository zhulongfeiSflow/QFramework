using QFramework;

namespace Game
{
    public class HomeModule : MainManager
    {
        protected override void LaunchInDevelopingMode() {
            // 开发逻辑
        }
        protected override void LaunchInTestMode() {
            // 测试逻辑
            // 加载资源
            // 初始化 SDK
            // 点击开始游戏
            GameModule.LoadModule();
        }
        protected override void LaunchInProductionMode() {
            // ⽣产逻辑
            // 加载资源
            // 初始化 SDK
            // 点击开始游戏
            GameModule.LoadModule();
        }
    }
}