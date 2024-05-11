using System.Text.RegularExpressions;

namespace VocabularySheet.Common.Extensions;

public static class RegexUtilities
{
    public static readonly Regex Numbers = new Regex(@"\d");
    public static readonly Regex Punctuations = new Regex(@"[\p{P}+=]+");
    public static readonly Regex Escapes = new Regex(@"[\t\n\r]");
    public static readonly Regex Latin = new Regex($"[a-zA-Z{UMLAUT_STRING}]");
    public static readonly Regex DoubleSpaces = new Regex(@"\s+");

    /// <summary>
    /// https://en.wikipedia.org/wiki/List_of_Latin-script_letters
    /// Section: Letters with diacritics
    /// Duplicates was removed
    /// </summary>
    private const string UMLAUT_STRING = "ẚÀàÁáÂâẦầẤấẪẫẨẩÃã̀́̂̌̍̎Āā̃̄̆̈̊ĂăẰằẮắẴẵẲẳȦȧǠǡÄäǞǟẢảÅåǺǻA̋aǍǎȀȁȂȃ̧̭̰̐̓Ąą᷎̱̥̇ẠạẬậẶặ̤Ḁḁ̯̩͔ȺⱥᶏꞺꞻⱭɑᶐBbḂḃ̒̕ḆḇḄḅ̬ɃƀᵬᶀƁɓ𐞅Ƃƃ̪ʙ̣CcĆćĈĉĊċČč͑̔ÇçḈḉꞔꟄ𝼝̨̦̮ȻȼꞒꞓƇƈɕᶝꜾꜿDdḊḋĎď̑ḐḑḒḓḎḏḌḍĐđᵭ�ᶁƉɖ�Ɗɗ�ᶑ�ƋƌȡꝹꝺᴅÐðÈèÉéÊêỀềẾếỄễỂểẼẽĒēḔḕḖḗĔĕĖėËëẺẻEeĚěȄȅȆȇȨȩḜḝĘęḘḙḚḛẸẹỆệ͕̜̹Ɇɇᶒⱸ̞ᶕᶓɚᶔɝƐɛƏə̏ɤFfḞḟᵮᶂƑƒꞘꞙꜰꝻꝼGgǴǵĜĝḠḡĞğĠġǦǧĢģ̫ꞠꞡǤǥᶃƓɠ�ɢʛ�ƔɣHhĤĥḢḣḦḧȞȟḨḩẖḪḫḤḥĦħꟸ�ꞪɦʱⱧⱨꞕ̢ʜɧ�ÌìÍíÎîĨĩĪīĬĭİiIıÏïḮḯǏǐỈỉȈȉȊȋĮįḬḭỊịƗɨᶤ�ᶖꞼꞽƖɩᵼJjĴĵǰɈɉꞲʝᶨɟᶡʄ�KkḰḱǨǩĶķḴḵḲḳᶄƘƙⱩⱪꝀꝁꝂꝃꝄꝅꞢꞣᴋĿŀ�LlĹĺĽľĻļḼḽḺḻḶḷḸḹŁłꝈꝉȽƚⱠⱡⱢɫꭞꞭɬ���ᶅᶪɭᶩꞎ�ȴʟ��ƛMmḾḿṀṁᵯṂṃᶆⱮɱᶬᴍǸǹŃńNnÑñṄṅŇňꞤꞥᵰ�ŅņṊṋṈṉṆṇ̲ƝɲᶮȠƞꞐꞑŊŋᶇɳᶯȵɴÒòÓóÔôỐốỒồỖỗỔổÕõṌṍṎṏȬȭŌōṒṓṐṑŎŏȮȯȰȱO͘oÖöȪȫỎỏŐőǑǒȌȍȎȏØø�Ǿǿ¸ƟɵᶱƠơỚớỜờỠỡỞở�ǪǫǬǭỌọỘộỢợᴓᶗꝌꝍⱺꝊꝋƆɔPpṔṕṖṗⱣᵽꝐꝑᵱᶈƤƥꝒꝓꝔꝕᴘQqꝖꝗꝘꝙʠɊɋRrŔŕṘṙŘřȐȑȒȓŖŗꞦꞧṞṟṚṛṜṝɌɍᵲꭨ�ɺ���ᶉɻʵ�ⱹɼⱤɽ�ɾ�ᵳ�ɿʀꝚꝛSsŚśṤṥŜŝṠṡŠšṦṧŞşȘșꞨꞩṢṣṨṩꜱſẛᵴ�ᶊʂᶳꟅⱾȿ��ẜẝᶋᶘʆ��TtṪṫẗŤťŢţȚț̗ṰṱṮṯṬṭƾ�ŦŧȾⱦᵵ�ƫᶵƬƭƮʈ��ȶᴛÙùÚúÛûŨũṸṹŪūṺṻŬŭUuÜüǛǜǗǘǕǖǙǚỦủŮůŰűǓǔȔȕȖȗƯưỨứỪừỮữỬửỰựŲųṶṷṴṵỤụṲṳɄʉᶶꞸꞹᵾᶙꭒꞾꞿʮʯɰᶭƱʊᵿVvṼṽṾṿꝞꝟᶌƲʋᶹⱱ�ⱴꝨꝩẀẁẂẃŴŵWwẆẇẄẅẘẈẉⱲⱳXxẊẋẌẍᶍỲỳÝýŶŷỸỹȲȳYyẎẏŸÿẙỶỷỴỵɎɏƳƴỾỿZzŹźẐẑŻżŽžẔẕẒẓƵƶᵶᶎꟆȤȥʐᶼʑᶽⱿɀⱫⱬƷʒǮǯᶚ�ƺʓÞþꝤꝥꝦꝧƻꜮꜯʡ�ʢ�ː�ˑ�";
}