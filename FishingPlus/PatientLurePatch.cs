using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace FishingPlus;

public class PatientLurePatch : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Minigames/Fishing3/fishing3.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {

        var patientLureCheck = new MultiTokenWaiter([
            t => t.Type is TokenType.CfIf,
            t => t is IdentifierToken {Name: "rod_type"},
            t => t.Type is TokenType.OpEqual,
            t => t is ConstantToken {Value: StringVariant { Value: "patient" } },
        ]);

        foreach (var token in tokens)
        {
            if (patientLureCheck.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.OpOr);
                yield return new ConstantToken(new BoolVariant(true));
            }
            else
            {
                yield return token;
            }

        }
    }
}

