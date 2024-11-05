using GDWeave;
using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace FishingPlus.Mods;

public class NeedFish : IScriptMod
{
    private string[] fishIDs;

    public NeedFish(string[] fishIDs)
    {
        this.fishIDs = fishIDs;
    }

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
        /* 
         * 










         */
        foreach (var token in tokens)
        {

            if (appendRolls.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.Newline, 2);

                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("roll");
                yield return new Token(TokenType.OpIn);
                yield return new Token(TokenType.BracketOpen);

                var i = 0;
                foreach (var fish in fishIDs)
                {
                    yield return new ConstantToken(new StringVariant(fish));
                    if (i++ < fishIDs.Length)
                    {
                        yield return new Token(TokenType.Comma);
                    }
                }
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("notif");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("AudioStreamPlayer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("new");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("notifsound");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 76);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("res://Sounds/store_enter.ogg"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("add_child");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("notif");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("notif");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("set_stream");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("notifsound");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("notif");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("volume_db");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.OpSub);
                yield return new ConstantToken(new IntVariant(-4));
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("notif");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("pitch_scale");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("notif");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("bus");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("SFX"));
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("notif");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("play");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("You rolled "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("roll");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(" !"));
                yield return new Token(TokenType.ParenthesisClose);


            }
            else
            {
                yield return token;
            }

        }
    }
}

