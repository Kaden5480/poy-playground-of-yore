#if BEPINEX
using BepInEx.Configuration;

#elif MELONLOADER
using MelonLoader;

#endif

namespace PlaygroundOfYore.Config {
    public struct Cfg {
#if BEPINEX

#elif MELONLOADER

#endif
    }
}
