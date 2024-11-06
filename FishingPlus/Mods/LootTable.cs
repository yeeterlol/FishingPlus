using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;


namespace FishingPlus.Mods;

public class LootTable : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var appendRolls = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "rolls" },
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "append" },
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.BracketOpen,
            t => t is IdentifierToken {Name: "roll" },
            t => t.Type is TokenType.Comma,
            t => t is IdentifierToken {Name: "s" },
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
        ]);

        
        foreach (var token in tokens)
        {
            if (appendRolls.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("FishingPlus");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("possible");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("Rolled "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("roll");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(" / Size: "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("s");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1);
            }
            else
            {
                yield return token;
            }

        }
    }
}

