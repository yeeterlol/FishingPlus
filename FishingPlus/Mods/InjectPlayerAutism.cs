using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace FishingPlus;

public class InjectPlayerAutism : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var busyCheck = new MultiTokenWaiter([
            t => t.Type is TokenType.CfIf,
            t => t is IdentifierToken {Name: "busy"}
        ]);


        foreach (var token in tokens)
        {
            if (busyCheck.Check(token))
            {
                yield return token;


                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("FishingPlus");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("on_focus_type");

            } else yield return token;
        }
    }
}

