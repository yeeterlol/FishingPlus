using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace FishingPlus;

public class InjectConfig : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc" || path == "res://Scenes/Minigames/Fishing3/fishing3.gdc" || path == "res://Scenes/Entities/Props/fish_trap.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var extendsWaiter = new MultiTokenWaiter([
            t => t.Type is TokenType.PrExtends,
            t => t.Type is TokenType.Newline
        ], allowPartialMatch: true);

        foreach (var token in tokens)
        {
            if (extendsWaiter.Check(token))
            {
                yield return token;

                yield return new Token(TokenType.Newline);

                yield return new Token(TokenType.PrOnready);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("FishingPlus");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.Dollar);
                yield return new ConstantToken(new StringVariant("/root/FishingPlus"));

                yield return new Token(TokenType.Newline);
            }
            else
            {
                yield return token;
            }
        }
    }
}

