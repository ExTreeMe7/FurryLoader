using System;
using System.Collections.Generic;
using Marsey.Patches;
using Marsey.Subversion;
using SS14.Launcher.Models.Data;

namespace SS14.Launcher.Marseyverse;

public class TitleCondition
{
    public required Func<bool> Condition { get; set; }
    public required string Message { get; set; }
}

public class TitleManager
{
    private const string BanMessage = "Banned";
    private static readonly Random Random = new Random();

    public static string GenerateTitle(DataManager cfg)
    {
        if (!cfg.GetCVar(CVars.RandTitle))
        {
            return LauncherVersion.Name;
        }

        var tagLines = new List<string>(TagLines);
        var titleConditions = new List<TitleCondition>
        {
            new TitleCondition { Condition = () => OperatingSystem.IsWindows(), Message = "if (OperatingSystem.IsWindows()" },
            new TitleCondition { Condition = () => Subverter.GetSubverterPatches().Count > 5, Message = "Subversion is superior to git you know" },
            new TitleCondition { Condition = () => Marsyfier.GetMarseyPatches().Count > 10, Message = "Marsyfiedisms" },
            new TitleCondition { Condition = () => cfg.GetCVar(CVars.FakeRPC), Message = $"{CVars.RPCUsername} RP" }
        };

        foreach (TitleCondition condition in titleConditions)
        {
            if (condition.Condition())
                tagLines.Add(condition.Message);
        }

        if (FullTitles.Count > 0 && Random.Next(4) == 0)
            return RandFullTitle();

        string name = RandTitle();
        string tagline = "";

        if (name == BanMessage)
            tagline = RandBanReason();
        else
            tagline = RandTagLine(tagLines);

        return name + ": " + tagline;
    }

    private static readonly List<string> Titles =
    [
        "Space Station 14 Launcher", "Dramalauncher",
        "Marsey", "Moonyware",
        "Marseyloader", "Robustcontrol",
        "Mirailoader", "Almost BepInEx",
        "FurryLoader", "MIT-certified funny",
        "戏剧装载机", "Oldest anarchy launcher in ss14",
        "ILVerifier", "Space Station 13",
        "BYOND", "Goonstation",
        "BYONDCONTROL", "Unitystation",
        "Stationeers", "RE:SS2D",
        "Schizostation 14", "Thaumatrauma",
        "Calamari v3", "hamaSS14",
        "FurStation 14", "OwO Loader",
        "UwUplink", "Softpaw Launcher",
        "Pawprint Protocol", "FluffOps Control",
        "BlueMoon Relay", "S.P.L.U.R.T. Terminal",
        "Lust Department Console", "Corvax Wega After Dark",
        "Wega Compliance Failure", "Nanotrasen HR Incident",
        "CentCom Furry Desk", "Robust Paw Control",
        "Marsey but Fluffier", "Subversion Tail Module",
        "The Forbidden Launcher", "ERP Containment Unit",
        "Fursona Authentication Service", "FluffOS Bootstrapper",
        "The Yiffstack", "Paw-coded Runtime",
        "Emergency Cuddle Shuttle", "No ERP In Maintenance",
        "The BlueMoon Build", "Syndicate Foxhole",
        "Clown Accepted The Collar", "HoS Saw The Logs",
        "AHelp Bwoink Simulator", "Warden's Least Favorite Window",
        "Cargo Ordered A Crate Of OwO", "Greytide With Ears",
        "The Tails Are Clientside", "FurryLoader Premium Furware",
        "NT Approved? Absolutely Not", "CentCom Denied The PR",
        "S.P.L.U.R.T. Station", "Citadel Station",
        "VORE Station", "Virgo Orbital Research Establishment",
        "Skyrat", "Bubberstation",
        "Coyote Bayou", "Desert Rose 2",
        "The Fluffy Frontier 18+", "Hyper Station",
        "Vesta of Orion", "Sojourn 13",
        "PampStation 13", "Nostration 13",
        "BlueMoon", "Whitemoon",
        "Howling Void", "Veilbreak Frontier",
        "Floof Station", "BlepStation",
        "Lust Station", "Stellar Stories",
        "FurStation", "Citadel After Dark",
        "VORE Station Console", "Skyrat Incident Desk",
        "Bubber Runtime", "Whitemoon Uplink",
        "Lust Station Loader", "Floof Station Bootstrap",
        "BlepStation Protocol", "Coyote Bayou Debugger",
        "Desert Rose Runtime", "Fluffy Frontier Patchbay",
        "Virgo Research Toybox", "OwO Login Gateway",
        "UwU Authenticator", "Rawr x3 Launcher",
        "Nuzzles Module", "Necky Wecky Control",
        "Bulge Detector 14", "Daddy Issues Runtime",
        "Murr Compliance Desk", "Pawprint Pleasure Dome",
        "Tail-Wagging Update Channel", "ERP Containment Breach",
        "LewdOps Control", "Lusty Lobby Manager",
        "Fursona Heat Sink", "After Dark Bootstrapper",
        "Code Blush Console", "Horny Jail Dispatcher",
        "Consent Form Terminal", "Toybox Access Panel",
        "Paw-coded Pleasure Stack", "Flustered Runtime",
        "The Forbidden Lobby", "Submissive Patch Notes",
        "Dominant Merge Conflict", "Softpaw Sin Engine",
        "Programmer Socks Runtime", "Thigh High Bootstrapper",
        "Femboy Build Pipeline", "Crop Top Compiler",
        "Oversized Hoodie Launcher", "Collar Configuration Manager",
        "Choker Auth Service", "Softboy Debug Console",
        "Pastel Terminal", "Blahaj Compliance Desk",
        "Cat-Ear Merge Tool", "Skirt Spin Scheduler",
        "Knee-High Patch Loader", "Thigh-High Dependency Graph",
        "Femboy CI Runner", "Collared Runtime",
        "UwU Stack Trace", "Nyea~ Build System",
        "Murr~ Release Channel", "TwT Crash Reporter",
        ">w< Update Manifest", "Soft Sweater Subverter",
        "Cropped Hoodie Runtime", "Collar Tag Validator",
        "Programmer Socks Protocol", "Thigh Highs Are Production Ready",
        "Femboy Friday Build", "Boykisser Release Desk",
        "Pastel Pawprint Launcher", "Softcore DevOps",
        "Femboy Ops Control", "Skirted Source Generator",
        "Top-Wearing Runtime", "Crop Top Release Manager",
        "Oversized Sweater Patchbay", "Thigh Gap Exception Handler",
        "Collar Bell Notification Service", "Paw Socks Compatibility Layer",
        "UwUplink But In Thigh Highs", "The Build Wears A Crop Top",
        BanMessage
    ];

    private static readonly List<string> FullTitles =
    [
        "Internal Affairs: OwO Division", "Station 14: Tail Edition",
        "Security Level: Blush", "FurryLoader: custom client toolkit",
        "FurryLoader: patches, packs, proxies", "FurryLoader: softer launcher, sharper tools",
        "FurryLoader: Marsey core, custom shell", "FurryLoader: resource packs with teeth",
        "FurryLoader: update channel online", "FurryLoader: title rotation active",
        "FurryLoader: launch, patch, customize", "FurryLoader: forked for control",
        "FurryLoader: client tooling, cleaned up", "FurryLoader: After Dark",
        "FurryLoader: OwO Edition", "FurryLoader: Lust Channel",
        "FurryLoader: BlueMoon Build", "FurryLoader: S.P.L.U.R.T. Mode",
        "UwU", "OwO",
        "TwT", ">w<",
        "nya~", "nyea~",
        "murr~", "mrow~",
        "awoo~", "hewwo",
        "pwease", "pwetty pwease",
        "murr~ build passed", "nyea~ update ready",
        "UwU manifest loaded", "TwT build failed",
        ">w< patch loaded", "OwO proxy connected",
        "mrow~ title refreshed", "awoo~ release found",
        "hewwo from the title bar", "pwease do not break CI",
        "pwetty pwease merge me", "UwU what's this asset",
        "OwO what's this diff", "TwT no release asset found",
        ">w< self-update available", "nyea~ debug mode",
        "murr~ patch mode", "UwUplink connected",
        "OwOverride enabled", "TwT recovery channel",
        ">w< compatibility mode"
    ];

    private static readonly List<string> TagLines =
    [
        "Marsey is the cutest cat", "Not a cheat loader",
        "PR self-merging solutions", "RSHOE please come back",
        "Today I learned RSHOE is datae, it has never been more over", "As audited on discord.gg/ss14",
        "Leading disabler of engine signature checks", "Sigmund Goldberg died for this",
        "Sandbox sidestep simulator", "DIY bomb tutorials",
        "incel matchmaking service", "#1 ransomware provider",
        "God, King & Bussy", "The mayocide is coming",
        "Leading forum for misandrists", "Largest long bacon provider",
        "天安門大屠殺", "Shitcode tester",
        "The code for the client literally calls itself a cheat client", "Tumblr client for fujoshis",
        "Cheap airfare and hotels", "Aliyah consulting",
        "Friday Night Funkin mod manager", "download .apk MOD (Infinite money, health, free admin)",
        "Disaster generator", "Primary cause of binary blob discussions",
        "George Bush did 9/11 and yet I'm the bad guy", "Hybristophiliac support group",
        "Space game but awesome", "Game SUCKS I go bed",
        "Go be ironic on wizden", "On payroll from the saudi government",
        "Handling the cheating situation since November", "World-class anti-cheat",
        "Leading cause of secondary psychosis", "they even made one for fastnoiselite which tells me some people have brain damage",
        "Station Announcement: Buttfucker69 (Medical Intern) has arrived at the station!", "Game's not out but there's 7,000+ ban appeals",
        "Notices update available", "Notices suspiciously fluffy diff",
        "Notices bulge in the patch list", "OwO what is this branch",
        "UwU committing directly to main", "S.P.L.U.R.T. compatibility layer loaded",
        "BlueMoon migration assistant", "Lust server incident report generator",
        "Corvax Wega after-hours compliance check", "Not a cheat loader, just very emotionally available",
        "Patch notes contain tail physics", "ERP disabled by default, allegedly",
        "The foxes have approved this build", "Do not ask why the changelog is purring",
        "Security found the collar in maints", "This build was reviewed by three tired furries",
        "CentCom says the ears are non-regulation", "Internal Affairs opened a cuddle investigation",
        "Nuke ops delayed by character customization", "The shuttle left because someone typed OwO in command",
        "RobustToolbox, but it has toe beans now", "Subverter found in the toybox",
        "Marsey API wearing a suspiciously soft hoodie", "Do not install patches from strangers in maints",
        "The patch list is wagging", "Hidesey? I barely know sey",
        "Local settings smell like vanilla and bad choices", "This client contains trace amounts of BlueMoon",
        "Compatible with emotional damage", "Now with 40 percent more questionable title text",
        "No ERP, only emergency roleplay paperwork", "The admin logs are looking at you",
        "Ahelp sound replaced with distant barking", "The captain has declared code blush",
        "Cargo ordered one crate of plausible deniability", "Wega legal team has left the chat",
        "Corvax taught me rules, BlueMoon taught me consequences", "S.P.L.U.R.T. called, they want their lobby back",
        "The foxgirl in command knows what you did", "FurryLoader: because normal titles were too dry",
        "Powered by coffee, patches, and bad decisions", "This window refreshes every 10 seconds, unlike your ban appeal",
        "Imagine reading the window title in 2026", "NT does not recognize your fursona as a valid ID",
        "The clown has been contained in the title bar", "If the title changes, the tail wags",
        "Strong ERP energy, zero ERP warranty", "Subtle? Never heard of it",
        "Legally distinct from a yiff client", "For research purposes, obviously",
        "Station AI law 5: protect the beans", "Do not the loader",
        "Forked, fluffed, and questionably maintained", "This is why CentCom stopped answering faxes",
        "The title bar is now an RP zone", "Syndicate encryption key says OwO",
        "Security confiscated the plushie as contraband", "Notices undocumented behavior",
        "Notices missing consent form", "Notices the repo is 8 commits ahead",
        "Notices maintainer fatigue", "Notices local-only excuses",
        "Notices the shuttle has arrived", "ERP is temporary, logs are forever",
        "The title bar has seen the evidence", "Ahelp received: stop being weird in arrivals",
        "Security cannot arrest the concept of OwO", "The warden is reading your examine text",
        "Your fursona failed SOP verification", "S.P.L.U.R.T. moment detected",
        "BlueMoon-grade decision making", "Lust-tier changelog hygiene",
        "Corvax Wega but the paperwork is blushing", "CentCom faxed back: no",
        "This PR requires a second adult", "The admin team has entered spectator mode",
        "Patch accepted, dignity rejected", "Legally distinct from ERP infrastructure",
        "The shuttle is delayed due to tail clipping", "Fork maintenance is my fursona",
        "I swear this is a launcher", "No refunds, only bwoinks",
        "The logs are purring ominously", "Rawr x3 loaded successfully",
        "Nuzzles the update manifest", "Notices suspicious bulge in the patch list",
        "OwO what is this server", "UwU please validate my content bundle",
        "Murr, the title bar is getting warmer", "Necky wecky compliance check passed",
        "ERP enabled in spirit, disabled in settings", "Citadel-grade decision making",
        "Skyrat-tier changelog energy", "Bubberstation sent a pull request",
        "BlueMoon called, it wants the lobby back", "Whitemoon saw the logs and blushed",
        "VORE Station ate the patch notes", "Lust Station approved the title rotation",
        "Floof Station says this is normal", "BlepStation handshake completed",
        "The tail physics are not part of the EULA", "Consent form missing from launch arguments",
        "Horny jail denied the appeal", "CentCom rejected the collar exemption",
        "The warden confiscated the plushie again", "Security level raised to code blush",
        "Do not ERP in departures", "The title bar is now breathing heavily",
        "The patch list is wagging for attention", "This build has strong after-dark energy",
        "Legally distinct from an ERP launcher", "No explicit content, only plausible deniability",
        "The toybox has been sealed by command", "Cargo ordered one crate of poor judgment",
        "The captain asked why the logs say OwO", "Internal Affairs opened a lewdness inquiry",
        "FurryLoader notices your custom engine", "Your resource pack is looking submissive and readable",
        "Your merge conflict needs discipline", "The update channel is acting bratty",
        "Patch accepted, dignity failed CI", "The repository is 18+ commits ahead",
        "The changelog has been nuzzled", "The release assets are looking breedable",
        "The title refreshes every 10 seconds, like a nervous tail", "The client is vanilla, the vibes are not",
        "Bwoink received: explain the title bar", "The admins can see the OwO",
        "The foxgirl in command clicked update", "The clown discovered BlueMoon",
        "The lawyer says this is ERP-adjacent", "The PR review got weird after line 69",
        "SOP does not cover this level of fluff", "The shuttle left because someone typed rawr",
        "AHelp response: stop nuzzling the engine", "The launcher purrs when the build passes",
        "The runtime is flustered but stable", "The content bundle is wearing a collar",
        "The proxy tab has suspiciously soft paws", "The update manifest smells musky, allegedly",
        "The code is clean, the comments are not", "The title bar has entered aftercare mode",
        "This fork requires hydration and supervision", "No ERP in maints, unless the maintainer approves",
        "The server browser needs a cold shower", "The patch loader is making eye contact",
        "This is why the logs need privacy settings", "The forbidden glossary has been indexed",
        "The ban appeal started with Rawr x3", "The lobby has failed the vibe check",
        "The fork is fluffed, forked, and slightly ashamed", "Rawr x3 notices the update",
        "Nuzzles your content bundle", "Pounces on the patch list",
        "You are so warm, little runtime", "Someone's happy to see Release mode",
        "Nuzzles the necky wecky of the manifest", "Murr, the build is getting bigger",
        "Rubbies the bulgy wolgy dependency graph", "Daddy likes clean commit history",
        "Paws on your bootstrapper", "Pwetty pwease pass CI",
        "Punish me with strict nullable checks", "The seven-inch itch was a missing semicolon",
        "The musky goodness was just stale cache", "Lickies the changelog respectfully",
        "Wiggles tail at successful publish", "Suckles on the tip of the version tag",
        "Eyes roll back at zero errors", "Balls deep in release engineering",
        "The build moans but still passes", "Programmer socks equipped",
        "Thigh highs improve compile time by 14 percent", "The build is wearing an oversized hoodie",
        "Crop top mode enabled", "Collar bell detected in debug logs",
        "UwU, the stack trace is blushing", "Nyea~, the manifest wants attention",
        "Murr~, release channel warmed up", "TwT, the tests failed softly",
        ">w< patch accepted", "Femboy Friday deployment window",
        "The CI runner has thigh highs", "This commit smells like pastel laundry",
        "The runtime is collared but stable", "The update manifest is wearing a choker",
        "Soft sweater, hard crash", "Programmer socks are now a build dependency",
        "The compiler saw the crop top and panicked", "The patch list is sitting politely",
        "The title bar is kicking its legs", "The fork put on knee highs and passed CI",
        "The release manager said nyea~", "The server browser is getting flustered",
        "The launcher asked for headpats", "The build is not submissive, just configurable",
        "The collar is cosmetic, the warnings are real", "The hoodie is oversized, the diff is tiny",
        "The socks are striped, the branch is clean", "The PR is wearing a skirt and has opinions",
        "The runtime purrs when nullable checks pass", "The updater wags when the asset name matches",
        "The title bar said UwU unprompted", "The crash reporter is crying TwT",
        "The dependency graph is >w< shaped", "The settings page is trying its best",
        "The proxy tab has soft paws and bad posture", "The patch loader is shy but effective",
        "The build pipeline has cat ears now", "The release notes are blushing in markdown",
        "The code formatter tucked in its shirt", "The launcher is cute, but the logs remember",
        "This title was compiled in thigh highs", "Softboy energy, release-grade output",
        "The femboy runtime requests validation", "Top, collar, thigh highs: build profile active",
        "Pastel hoodie detected near critical infrastructure", "The commit history needs aftercare",
        "This branch is wearing programmer socks", "The status bar is trying not to stare",
        "The tests are jealous of the socks", "The release is a little pre-releasey",
        "Pre-release fluids detected", "Pre-release channel looking suspicious",
        "Pre-release build, post-nut clarity", "Pre-release stains on the changelog",
        "Pre-release drips through CI", "Pre-release candidate is getting needy",
        "Pre-release tag came early", "Pre-release branch is leaking intent",
        "Pre-release notes need a towel", "Pre-release build wants validation",
        "Pre-release artifacts are sticky", "Pre-release pipeline is breathing heavily",
        "Pre-release channel failed containment", "Pre-release runtime is too excited",
        "Pre-release asset uploaded prematurely", "Pre-release mode: wipe logs after use",
        "Pre-release flavor text, release-grade shame", "Pre-release title bar detected moisture",
        "Pre-release but make it UwU", "Pre-release candidate says murr~",
        "Pre-release went >w<", "Pre-release TwT recovery mode",
        "Pre-release collar fitting complete", "Pre-release hoodie privileges granted",
        "Pre-release programmer socks equipped", "Pre-release thigh highs passed QA",
        "Pre-release paws on the bootstrapper", "Pre-release branch needs discipline",
        "Pre-release merge conflict wants attention", "Pre-release tags are for good boys"
    ];

    // Have I truly lost any imagination? Yeah, kind of.
    private static readonly List<string> BanReasons =
    [
        "Using a modified client", "Self-antag",
        "Ban evasion", "Datacenter",
        "Mirrored ban from nyano", "Vac banned from secure server",
        "Unpleasant to deal with on github and ingame", "As CMO participated in slave RP",
        "Left seconds after receiving an ahelp.", "Claiming to be hitler of jamaican jazz",
        "eat shit you sick fuck", "GRUGSTEIN",
        "alt", "welderbombed a group of sec",
        "took a welding tank for a walk", "raider",
        "mass murdered people with the gib stick from a present", "overall self-antag and grief of the security department",
        "Gifted a chicken mob to warden, calling it \"BBC\"", "Cult. Appeal in 6 months.",
        "Literally has 20+ accounts.", "RP'ing as jocks beating up (killing) nerds (5) and shoving them into lockers",
        "Character named \"ADOLF RIZZLER\"", "wrote on a piece of paper \"I pissed myself\"",
        "Gross imcompetence as captain", "Giving out aa ids",
        "Being swept", "Being karma",
        "teaching people how to make schedule 1 narcotics", "Hitler RP in cargo orders",
        "Selling the nuke", "Actively trying to build up a room of miasma",
        "Set AME to 140 then Disconnected", "Exploiting. Teleported into armory using chairs.",
        "rule 0 permaban speedrun world champion ", "Take a break, bud",
        "No-appeal perma ban based on advice of the player and other hosts", "Metacomms",
        "\"soft antag rolling,\" powergaming, being rude in OOC", "Warned multiple times about powergaming as antag roles",
        "found to have been soft-antag rolling", "made the Janitor a furry against his will",
        "found to be displaying unhealthy investments in rounds based off of OOC/deadchat commentary", "Mass-RDM. Killed 31 people using electrified grilles",
        "Openly bragging about being a shitter", "Posting fake porn links isn't funny",
        "3 bans in 6 months threshold", "factors leading to a lot of attention seeking behavior",
        "Did nothing but plasmabomb med for 35 minutes", "asking people to do the thug shaker"
    ];

    private static readonly List<string> Actions =
    [
        "Pumping nitrous into distro", "Injecting plasma into batteries",
        "Slipcuffing hos", "Starting a cult",
        "Rolling for antag", "Plasmaflooding station",
        "Declaring cargonia", "Evading bans",
        "Taking fuel tanks for a walk", "Gibbing for no reason",
        "Bullying trialmins", "ANNOUNCING PRESENCE",
        "Petting Marsey", "Gossiping about spaceman drama",
        "Porting /vg/ to 14", "Granting +HOST",
        "Playing Balalaika", "Anime past 2018 is slop",
        "Trading Oil", "Abusing Roskomnadzor",
        "Making fun of people on mastodon", "Hacking the powernet",
        "Overthrowing the captain", "Building a mech",
        "Creating AI laws", "Running diagnostics on the engine",
        "Upgrading the medbay", "Securing the armory",
        "Repairing hull breaches", "Conducting xenobio research",
        "Exploring lavaland", "Mining for plasma",
        "Crafting improv shells", "Setting up the SM",
        "Breaching into secure areas", "Fighting syndicate operatives",
        "Escaping on the shuttle", "Reviving dead crewmembers",
        "Cloning the clown", "Rushing the captain's spare ID",
        "Sabotaging telecomms", "Disposal bypassing into armory",
        "Exploiting pulling", "Refreshing tail physics",
        "Validating pawprints", "Checking Wega paperwork",
        "Auditing ERP containment", "Loading BlueMoon rumors",
        "Indexing S.P.L.U.R.T. references", "Sanitizing OwO logs",
        "Rebuilding the cuddle cache", "Patching emotional support modules",
        "Negotiating with CentCom HR", "Hiding the collar from security",
        "Rebinding the AHelp panic key", "Running the fursona migrator",
        "Compiling suspiciously soft code", "Reloading the title bar",
        "Applying code blush", "Generating plausible deniability",
        "Inspecting maints for fox activity", "Updating the forbidden glossary",
        "Compressing bad decisions", "Checking if the clown consented",
        "Reticulating yiff splines", "Restoring wholesome alibi",
        "Synchronizing paw-coded runtime", "Converting drama into release notes",
        "Measuring fluff density", "Escalating to the cuddle department",
        "Downloading one more questionable asset", "Refusing to elaborate",
        "Opening the toybox", "Closing the toybox very quickly",
        "Nuzzling update manifest", "Wagging title bar",
        "Sniffing release assets", "Polishing the toybox",
        "Refreshing code blush", "Indexing S.P.L.U.R.T. lore",
        "Loading BlueMoon alibis", "Auditing Citadel paperwork",
        "Parsing Skyrat incident logs", "Checking Bubberstation compatibility",
        "Flustering the runtime", "Applying after-dark patchset",
        "Validating consent forms", "Escalating to horny jail",
        "Rebinding the OwO key", "Calibrating tail physics",
        "Synchronizing paw telemetry", "Hiding plushie from security",
        "Compressing lewd changelog", "Cooling down the server browser",
        "Reloading the collar exemption", "Sanitizing suspicious examine text",
        "Converting thirst into release notes", "Asking CentCom for aftercare",
        "Negotiating with the cuddle department", "Rotating the title bar submissively",
        "Making the patch list blush", "Refusing to explain the logs"
    ];

    public static string RandTitle() => Titles[Random.Next(Titles.Count)];
    public static string RandFullTitle() => FullTitles[Random.Next(FullTitles.Count)];
    public static string RandTagLine() => RandTagLine(TagLines);
    private static string RandTagLine(IReadOnlyList<string> tagLines) => tagLines[Random.Next(tagLines.Count)];
    public static string RandBanReason() => BanReasons[Random.Next(BanReasons.Count)];
    public static string RandAction() => Actions[Random.Next(Actions.Count)];
}
