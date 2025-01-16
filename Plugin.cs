using System;
using System.Collections.Generic;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using CommandSystem;
using PlayerRoles;
using MEC;


namespace SCP079_Escape
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "Escape as SCP-079";
        public override string Author => "DuXuu";
        public override Version Version => new Version(1, 1, 0);

        [CommandHandler(typeof(ClientCommandHandler))]
        public class NameCommand : ICommand
        {
            public string Command { get; } = "Escape079";
            public string[] Aliases { get; } = null;
            public string Description { get; } = "Allows SCP-079 to escape the facility after reaching maximum level.";


            public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
            {
                Player player = Player.Get(sender);

                if (!(player.Role.Type.IsScp()))
                    response = $"<b><color=#960018>Musisz grać SCP-079 aby użyć tej komendy!</color></b>";
                else
                {
                    var SCP079 = player.Role as Scp079Role;

                    if (SCP079.TierManager.AccessTierLevel == 5)
                    {
                        response = $"<b><color=#228B22>Pomyślnie rozpoczęto proces ucieczki.</color></b>";

                        Timing.RunCoroutine(TimeToEscape(30, player));

                        Timing.CallDelayed(20f, () =>
                        {
                            Cassie.DelayedMessage("pitch_1.1 .g1 .g6 .g3 .g4 .g3 .g2 pitch_1 .g2 pitch_0.9 .g6 .g5 .g2 pitch_0.7 .g1 .g3 .g2 pitch_0.4 .g1 .g4 pitch_0.1 .g4 pitch_0.85 jam_010_2 Warning . . . pitch_0.1 .g4 pitch_0.85 SCP 0 7 9 jam_044_2 activity in facility systems has stopped . jam_044_3 Scanning facility area in 3 pitch_0.4 .g5 .g6 pitch_0.8 . . 2 pitch_0.4 .g5 .g6 pitch_0.7 . . 1 pitch_0.4 .g5 .g6 pitch_0.6 . . . . pitch_0.2 .g6 .g2 .g1 pitch_0.75 Critical warning . pitch_0.7 alert . pitch_0.2 .g4 .g1 pitch_0.7 Critical warning . pitch_0.9 Facility devices detected SCP 0 7 9 code outside of site 0 2 . pitch_0.6 repeat . pitch_0.85 jam_060_3 SCP 0 7 9 has breached out of facility . jam_020_2 . . . . pitch_0.2 .g4 . pitch_0.05 .g5", 1);
                        });

                        Timing.CallDelayed(30f, () =>
                        {
                            player.ShowHint($"<b><color=#228B22>Udało Ci się uciec jako SCP-079!</color></b>", 10f);

                            Map.TurnOffAllLights(999f);

                            player.RoleManager.ServerSetRole(RoleTypeId.Spectator, RoleChangeReason.Escaped);
                        });

                    }
                    else
                        response = $"<b><color=#8137CE>Musisz mieć maksymalny poziom dostępu, aby uciec z placówki.</color></b>";
                }
                return true;
            }

            private IEnumerator<float> TimeToEscape(int time, Player player)
            {
                for (int i = time; i > 0; i--)
                {
                    if (i >= 5)
                        player.ShowHint($"<b><color=#228B22>Czas do ucieczki: {i} sekund. </color></b>", 1f);
                    else if (i >= 2)
                        player.ShowHint($"<b><color=#228B22>Czas do ucieczki: {i} sekundy. </color></b>", 1f);
                    else
                        player.ShowHint($"<b><color=#228B22>Czas do ucieczki: sekunda. </color></b>", 1f);

                    yield return Timing.WaitForSeconds(1f);
                }
            }
        }
    }
}
