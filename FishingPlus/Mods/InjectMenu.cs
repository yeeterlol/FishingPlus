using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace FishingPlus;

public class InjectMenu : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/HUD/playerhud.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var extendsWaiter = new MultiTokenWaiter([
            t => t.Type is TokenType.PrExtends,
            t => t.Type is TokenType.Newline
        ], allowPartialMatch: true);
        var readyWaiter = new FunctionWaiter("_ready");
        var notUsingChat = new MultiTokenWaiter([
            t => t is ConstantToken {Value: StringVariant { Value: "" } },
            t => t.Type is TokenType.Newline & t.AssociatedData is 4,
            t => t is IdentifierToken {Name: "_exit_chat"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline & t.AssociatedData is 2,
            t => t.Type is TokenType.Newline & t.AssociatedData is 2,
            t => t.Type is TokenType.CfIf,
            t => t.Type is TokenType.OpNot,
            t => t is IdentifierToken {Name: "using_chat" }
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
            else if (readyWaiter.Check(token))
            {
                yield return token;

                yield return new Token(TokenType.Newline, 1);

                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("main");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("in_game");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("HBoxContainer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("add_child");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("FishingPlus");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("the_button");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("instance");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);

                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("main");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("in_game");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("add_child");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("FishingPlus");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("mod_panel");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("instance");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);

                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("button_instance");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("main");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("in_game");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("HBoxContainer");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("fishingplus");

                yield return new Token(TokenType.Newline, 1);

                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("panel_instance");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("main");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("in_game");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("modpanel");

                yield return new Token(TokenType.Newline, 1);
                yield return new IdentifierToken("button_instance");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("connect");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("pressed"));
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("panel_instance");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("_open"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1);


            }
            else if (notUsingChat.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("FishingPlus");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("on_focus_type");
            }
            else yield return token;
        }
    }
}

