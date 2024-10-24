using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace FishingPlus.Mods;

public class FishChance : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var spawnFishMatch = new MultiTokenWaiter([
            t => t.Type is TokenType.CfIf,
            t => t.Type is TokenType.BuiltInFunc,
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.OpGreater,
            t => t is IdentifierToken {Name: "fish_chance" },
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline,
        ]);

        foreach (var token in tokens)
        {
            if (spawnFishMatch.Check(token))
            {

                yield return token;
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("Current Fish chance: "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("fish_chance");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(100));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant("% / "));
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant("Type: "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 2);
            }
            else
            {
                yield return token;
            }

        }
    }
}

