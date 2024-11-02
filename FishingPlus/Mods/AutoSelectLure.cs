﻿using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace FishingPlus.Mods;

public class AutoSelectLure : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var fishingRodCheck = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_cast_fishing_rod"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline
        ]);
        /*
         *  6108: Token(Newline, 1)







  6160: Token(OpAssign, )
  6161: IdentifierToken(Identifier, 436, best_bait)
  6162: Token(Newline, 3)
  6163: IdentifierToken(Identifier, 74, PlayerData)
  6164: Token(Period, )
  6165: IdentifierToken(Identifier, 299, emit_signal)
  6166: Token(ParenthesisOpen, )
  6167: ConstantToken(Constant, 234, StringVariant(_bait_update))
  6168: Token(ParenthesisClose, )
  6169: Token(Newline, 1)
        */
        foreach (var token in tokens)
        {
            if (fishingRodCheck.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("BAIT_DATA");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("bait_selected");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("catch"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("best_bait");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant(""));
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("baits");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("bait_inv");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("bait_inv");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("baits");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("best_bait");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("baits");
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("best_bait");
                yield return new Token(TokenType.OpNotEqual);
                yield return new ConstantToken(new StringVariant(""));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("bait_selected");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("best_bait");
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("emit_signal");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("_bait_update"));

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

