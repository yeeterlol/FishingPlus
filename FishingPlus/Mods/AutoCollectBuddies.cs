using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace FishingPlus.Mods;

public class AutoCollectBuddies : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Props/fish_trap.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {

        var buddyCheck = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "PlayerData"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "_send_notification"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is ConstantToken {Value: StringVariant { Value: "one of your buddies caught something!" } },
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline
        ]);
        
        foreach (var token in tokens)
        {
            if (buddyCheck.Check(token))
            {
                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("FishingPlus");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("autocollect");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("the_fish");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Globals");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("item_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("roll");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("file"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("item_name");

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("fish_quality");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("QUALITY_DATA");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("quality");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("name");


                yield return new Token(TokenType.Newline, 2);
                // PlayerData._send_notification("you caught a " + roll + " with size " + str(size) + " and quality " + str(quality))
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("you caught "));
                yield return new Token(TokenType.OpAdd);
                yield return new IdentifierToken("the_fish");
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(" / size "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(" / quality "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("fish_quality");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                // 	has_fish = false
                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("has_fish");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                // 	$caught.visible = false
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("caught");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("visible");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                // _send_item()
                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("_send_item");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                // fish_data.clear()
                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("fish_data");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("clear");
                yield return new Token(TokenType.ParenthesisOpen);
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

