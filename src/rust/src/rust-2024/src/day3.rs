// Copyright (c) Christopher Tisdale 2024.
//
// Licensed under BSD-3-Clause.
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      https://spdx.org/licenses/BSD-3-Clause.html
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#[must_use]
pub fn part1(input: &str) -> usize {
    let mut result: usize = 0;
    for line in input.lines() {
        let mut iter = line;
        while let Some(start) = iter.find("mul(") {
            if iter.len() - 1 == start {
                break;
            }

            iter = &iter[start + 4..];
            result += parse_multiply(iter);
        }
    }

    result
}

#[must_use]
pub fn part2(input: &str) -> usize {
    let mut enabled: bool = true;
    let mut result: usize = 0;
    for line in input.lines() {
        let mut iter = line;
        while enabled && iter.contains("mul(") || iter.contains("do()") || iter.contains("don't()")
        {
            let start_do = iter.find("do()").unwrap_or(usize::MAX);
            let start_dont = iter.find("don't()").unwrap_or(usize::MAX);
            let start = iter.find("mul(").unwrap_or(usize::MAX);
            if enabled && start < start_do && start < start_dont {
                if iter.len() - 1 == start {
                    break;
                }

                iter = &iter[start + 4..];
                result += parse_multiply(iter);
            } else if start_do < start_dont {
                iter = &iter[start_do + 4..];
                enabled = true;
            } else if start_dont != usize::MAX {
                iter = &iter[start_dont + 4..];
                enabled = false;
            } else if !enabled {
                break;
            }
        }
    }

    result
}

fn parse_multiply(input: &str) -> usize {
    if let Some(end) = input.find(')') {
        let local = &input[..end];
        let split = local.split(',').collect::<Vec<&str>>();
        if split.len() != 2 {
            return 0;
        }

        let left = split[0].parse::<usize>();
        let right = split[1].parse::<usize>();
        if let (Ok(left), Ok(right)) = (left, right) {
            return left * right;
        }
    }

    0
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn part_1_sample() {
        let result =
            part1("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))");
        assert_eq!(result, 161);
    }

    #[test]
    fn part_1_measure() {
        let result = part1(MEASURE);
        assert_eq!(result, 174_561_379);
    }

    #[test]
    fn part_2_sample() {
        let result =
            part2("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))");
        assert_eq!(result, 48);
    }

    #[test]
    fn part_2_measure() {
        let result = part2(MEASURE);
        assert_eq!(result, 106_921_067);
    }

    const MEASURE: &str = "why()}''(!how()$~mul(420,484) ]}}mul(218,461),]/select()mul(93,56)';$-;*#$mul(162,415)mul(556,374)when()~when()<[select()^<(@mul(561,946);mul(97,699)select()+%when()~who():mul(387,15)>mul(927,207)~>~when()*who()'do()mul(454,740)when()%from(),~@%]from()mul(54,688)mul(338,694)what()select()~!< <;+<mul(127,722)'how()#~%*^mul(337,149)!,!mul(11,87)'<who()* where(671,579)-mul(596,125)who(){@,,-;+from()how(148,934)mul(452,741) ~}mul(513,343)mul(45,508),where()what()mul(758,167)$@''where()!*from()?mul(3,372)$@mul(491,647)]':%why()mul(459,967)(#(mul(369,467)):mul(662,431))<]:from()select()@mul(172,72)why()+,who()#from()+what()@#mul(394,721)what(){why()'!mul(69,419)when(300,372)%where()mul(135,896)who()who()when()}(?&%when()^mul(692,658)@~!$*when()+!mul(586,546)?#$select()from()~/mul(609,19)from()where():who()~*}mul(54,319);what()<what()when()when()@^<:mul(962,480)[%how(133,773)what()'^!:#]mul(419,406)@}#!)'mul(524,802)!%mul(938,46)$~{#mul(443,398)~where()*}&{]&>/mul(373,536)&+mul(505,931)why()[mul(457,381) >/ mul(800,67)why()~what()[mul(807,815)&$who()mul(667,529)&how(843,372)what()mul(636,823)<mul(363,915)$ +mul(162,118)($/{when()'^what()mul(461,357)^{mul(303,284)how()?why()mul(31,429)$who()-do()what()[}#mul(471,260)!,^(?,- why()!mul(706,849)(mul(845,857)@;?mul(417,923)~from()%how()who()where()&mul(731,874){![]+mul(433,314)what()>?*who()mul(960,331)where()?mul(648,668)how()/<!why()>who()why()!'mul(649,819)[~:how()&what()]{mul(857,238)%-mul(603,559)mul(511,89):mul(888,328)*how()$/}]mul(177,966)who(777,724)why();:;mul(211,756)]:}+mul(297,394)>^how();$[mul(603,264)mul(794,883)when()why()~&select()from())mul(446,859) & ;:>?*what()/mul(388,763);},?when()do()where())<->what()mul(974,397)+why()select()#@mul(137,814)@when()why()when(697,786)mul(897,431)^}}&mul(74,810)?<&~] '&$]mul(49,565)(>where())]/mul(926,812)mul(842,573) how()-mul(126,526) @mul(818,934)?select()}^what()who()from()}mul(240,118)!from()^@?what()mul(77,983)+$what()mul(736,950)$&%?why()select();mul(213,409)where()*&*what():>~when()<mul(581,188)%]where()-?[why(308,248)>when()mul(87,245)$^ what()]&#select()mul(558,637)why()!mul(695,929[;*)+??mul(896,494)when())who()select()^>+how(641,113)mul(374,932);^?:where()>mul(780,265)^#;+when()}}mul(218,272)@$#mul(892,55)[what()?~{'when()}mul(527,984) {>?-'+-:-don't()select()#;/?]''!mul(56,714)/<select()*)>why()>mul(819,178)*~[+}],mul(793,717)from(75,849)~-,+($/,mul(719,587)where()#:@;*$+#?mul(919,859)who()+what(),  @;>mul(89,488)where()?from()#$mul(680,657)mul(619,642)?[+don't()$:]{select()$what()@mul(619,164)}!select()mul(528,754)mul(199,830)how()where()+from()/~$who()(mul(49,273(mul(32,974)do()*select()]mul(42,960)%@?:[##::mul(802,384)]{##where())what()[mul(610,271)how()#@>what()where()don't()'&)>mul%?mul(449,76)when()%[<{^mul(944,356)what()(*;<{$<~mul(476,918){who(){-mul(124,698);?who()who(610,775)what()'&mul(579,336)why()<{'mul(953,943)<from()mul(222,14)?don't()'why(){mul(986,323)$who()mul(476,802)-<(from(577,504)when(377,110)%mul(160,849)why()when()what()mul(239,985);<who()%!mul(356,867)why(87,771)-mul(486,916)
'@$from():do()* select()why():how()~/mul~!^,from()#mul(601,494)%<,^mul(549,744)?!+/mul(381,237)+what()when()[)mul{~'~*what()from()$;#,mul(368,89){[;?how()why()#^%mul(926,974)how()select()when()when()@:{,do()how()from()from();;~when()<( mul(770,708)from()# ??($!:mul(612,548)$why())(who()%mul(776,783who()/%'>:%mul(651,439)/who(870,472)from()select()( -] $mul(16,267)mul(865,452)!select()who(){)$%$mul(888,41)],when(737,918)[#@#'mul(655,759)}/$(?#do()when()how()mul(160,406)]$mul(392,433)([;!##^from()mul(56,722)what()*#%select()(>#<who()mul(766,7)$mul(349,492) from()where();-}mul(12,290) -mul(268,832)~@what()when()!)@mul(994,952)?)'when()#'+mul(218,644)^^@ [mul(231,58)'[*/ mul(851,263)~select()#who()<#+where()who()mul(66,566)mul(347,173){why()%:what()]how()mul(266,892)}^mul(399,289)how()]@#]}#don't()$mul(337,947)^['[+-<}}'mul(556,447)&why()[when(193,903)select()mul(269,222)>mul(554,881)'mul(355,774)&/mul!~*<mul(900,983)/ select()'%?mul(112,191)*$&<who()when()when())mul(670,76)>)mul(32,426)where()[how()how()mul(160,991)(where()when()who()^{/why()mul(638,720)how()]]when()mul(617,160select()mul(728,694)+{why()mul(173,600)mul(846,617)?mul(630,82)select(450,31)mul(443,274){; ]when()-(#mul(274,844));];mul(69,98)how()(>#-select()%mul(121,360)!^! :<~why()#'mul(662,319):&%%%/;where(58,840)how()mul(140,256)#<$!when()how() <<mul(690,207)how():{<>!(who()do()}!&?#(>why()~mul(914,702);who(222,816)how(){(&('^;mul(881,879)who()mul(930,525)when(),when()mul(882what()&from():where()select(){>?&mul(535,202)/[<what()how()?mul(499,269)()%'[}mul(855,63)<-why()*?*mul(956,752)#;{!,#}how()don't();what()>~'[?#<^mul(195,514)#!mul(69,391)!,why()[,#~!,select()mul(732,760'[^?-^]>mul(52,825)when()(^where()]select()@do()#why()mul(287,62),who() &why()mul(24,517)#) from()what()mul(411,475)what()who()~$&mul(963}*&^,what()mul(162,897)!@~~@mul(985,450):%}-$where())when()mul(164,716)mul(211,564);mul(886,158)where()% '~select()@select())mul(215,209)%from(){select()who()mul(310,717/^$why(414,216)#&,why()select()@mul(765,613)where()^#}#@when()who()@why()mul(320,608)*mul(572,911)do())>:&%;mul(172how())%when()+@%@<mul(829,161),-:mul(304,894)}?*><%[mul(279,942)%#*;('#mul(838,154)/?what()when()&;@%^mul(65,418)<mul(308,921)[$($when(597,167)#)when()mul(355,197)~@'from()when(425,591)>why(307,491)mul(252,964)#mul(428,463)@from()mul(463,946){&where(649,23)+&who()$?),mul(545,536)#how()$?do()?mul(799,451)->%@what()!~mul(685,980)why()<<{!,)[mul(542,862)?),]>mul(702,259)who()^@mul(563,554)}%:}]}-$how()mul(819,791)#do()#:[:(what()how()}&mul(753,669)<{] do() /where()~where()%*(mul(561,517)~@where(82,621)~&mul(21,397)%mul(585,719)]!$where())!^when()who()who(587,725)mul(694,254)'-[+>{:;mul(650,398)select()%mul(549,891)$: #-how())}}from()don't()what()#mul(90,910)mul(903,233)mul(547,208)[-:mul(368,116)what()>*{}select()mul(579,530)&?!/^mul(982,408))~;how()&mul(68,361)when()where() %)from()mul(830,253how(),mul(316,679)('/mul(147,773)<mul(179,83);(^mul(577,204)mul(360,592)]why()/:!<~-who())mul(888,688)}]mul(271,431)mul(991,736)what()[+{'~?from()><mul(520,174) !!$what()what()&{mul(791,96) &>)what()')^+?mul(365,155)#}(who()mul(141,657<when()(]#] }mul(701,322)mul(677,714)mul(313,947),who()mul(229,775)^ mul(704,688)< +![>,]@mul(437,176)where()':*%'')mul(420,497)
?)why(473,359)mul(550,773)why()who()%why()>;;)mul(47]{?mul(686,382)]&]%-)+&mul(540,547)how()mul(21,121)$[-/@mul(894,561)^why()~!''mul(76,341)&#why() mul(867,180)]@[@select()&why()^)mul(328,567)why()mul(500,955)mul(602,93)! #who()*~mul(395,822)?when()~[when():&what()from()mul(26,996)how()?from()[who()[how();,what()mul(127,564)+how()]>*)mul(326,149)}*mul(741,743)what()/-,where()select()$-mul(571,104)~mul(557,104)from()/]^&>how(){[don't()>#mul(243,161)what()mul(784,800)when()>%%^?]mul(969,40)what()$#why(201,806)+mul(287,102)mul(581,311)select()'who()@{^+why()mul(598,245))/?;${who(972,431)mul(766,217)select():}<mul(702,474)what()/-when(228,647)%mul(553when())}[why()mul(454,895)do()where():mul(169,35)({@^mul(478,718)}#*+mul(549,42)$(mul(32,8)($*) from()mul(470,783)?/;~% %mul(354,577)%$@don't()!^where():%'(mul(241,993)from()who();how()+mul(836,571)/+>from()/when()/mul(746,688)?}@(<*what()mul{%#;from())from()why()mul(723,608)*]@{who()what()how():where()*mul(726,41)select();:what()~'how();~how()mul(352,571)>);from()]%don't())how()what()*]])who()what()mul(104,291)}:'why()where()what()<)!mul(294,381)[!~!@'mul(617,169)select()mul(597,322)@+:'<%@select()?from()mul(751,110)$^what(),?('who(375,411)mul(487,642)!$mul(602%?who()who()*why(517,725)who()%from()+mul(611,293)?where()who()*what()mul(572,491)%#^why()@why(559,20)mul(147,928@(how()$when();?{{;where()mul(235,699)]/don't()~>& mul(522,362)how()select()how()/,mul(808,332){@>mul(382,736)from()/%{mul(763,385)-&![+[-[mul(493,959)}$,what()%/}:mul(999,264)#:'what(923,646)<!mul(823,219)where(747,565)[how()where()mul(954,651)]'(when()$&how()<mul(126,796)&mul(200,914)what()}mul(97,838)>how()mul,(({( !)^mul(774,947)mul(825,424)'when()&who(),?{'!:mul(645,624)({$}mul(755,196)how(256,304),^+mul(78,987)]mul(155,256)~mul(643,546)when()@;^&!%#mul(464,137)&{mul(549,85):#]}from()^where()from()from()*mul(471,24~$when()when()how(),mul(824,94)how()where()why() /&from()?mul(232,49)' *mul(587,107)[{why()[from()%when(17,197);]+mul(728,869)what()how():mul(85,612):)#;where(),>mul(992,351)mul(149,626)*;}]mul(185,920)mul(258,257)'how()]@select():( how()>mul(891,488)}when()${~how()mul(69,62)/;do()$;?how(548,712)%mul(163,414)<where()@mul(238,10)((:& select()mul(331,689)]*)where()select()#{?mul(751,187), -%from()(;mul(114,913)@<#*why()';mul(890,554))mul(367,963)-where()mul(374,638)(where()mul(651,193)'select():where()([;where(293,408);,do()/~~%when()why()+why()mul(409,836)* # :mul(314,762!-@-%//^#$do()$^]!;:mul(917,894]-why()when(126,130) [mul(462,596)%&,where()/:mul(258,244)where()):<,[where()mul#what()how(436,462);[~mul(862,325);</when())+}when()mul(355,83)& mul(996,680)>&~do()$:where()select()*why()*/#,mul(245,774) ^where(458,665)] --what()mulwho())~'^?^don't()){}#&<]~mul(894,110)/mul(28,62)%^&*'?what()%select()mul(490,609)[do())$when()@{$] mul(446,48)}when()+:(>'[mul(975,915)mul(282,257)}mul(504,101)what()$)$select()mul(799,76)~/mul(157,178):%why()who() $>+mul(136,339)'-;[>mul(424,206)(-what()-why()#$mul(855,735)when()%]?when())#~*)mul(422,548)mul(403,999)'&( ')select()mul(262,876)&'&}^]what()'~mul(304,364)&)when(472,742)mul(923,176)mul(431,469) #;why(){?mul(330,297)mul(328,720,!from()who()how()[mul(92,961)where()>;mul(157,314-&{:>~{&^?mul(91,266)$~:[mul(875,759)?<$when()%,&[#mul(214,511)where()mul%,)@how()@ ]how()#&mul(506,250)how()from(){,,?]]&(mul(67,306) ^-do()mul(87,147)
#!/usr/bin/perl&~?mul(433,7)^select()don't()($who()>->{mul(572,795)>#}what()mul(428,863)#~mul(408,652)from()when() why()+/#@select()[mul(940,728)who()[how()mul(246,59) ;}+mul(498,374),:mul(489,557)mul(737,804)where()?@#&@where(623,413)!mul(328,754)~'?mul(612who():^$(mul(72,123)',&++who(208,919)mul(18&mul(575,80)when(835,393)why()when()select()mul(362,743>$from(95,887)%><when(147,683)'*where()mul(490,263)$#where()#when()@&mul(491,393)why()#>&[;do()$]from()&-what(),who()@)mul:#why()?[':?~ ^mul(633,858)&where()where())#^-from(252,913)mul(126,973)%from()>-  %mul(670,112)<%{<how()$mul(353,742)+%how()-)why(369,283)mul(47,808)$@mul(774,434) who()@>from();)(mul(386,975)][#-@mul&)where()who(),mul(790,566)mul(542,715))mul(264,826',mul(935,548)how(),why()}(}mul(164,558)})select()who()@select()(mul(165,31)?>?;,~+*<$mul(341,516)%/mul(147%when()why(611,793)]mul(162,61)when()+how()'%why(409,154)mul(571,437)mul(522,596)/how()select()how(29,352)why()what():mul(467,537),how()]*^,%mul(974,626)~ $when()*select()+mul(424,960)from()(what() &;from()how() mul(332,298)+![where();select();from()(mul(613,817)^#when():;~;:)mul(632,893)how()>mul(211,720)how()!*+)]from()what(){mul(833,547);(  /how(473,744)mul(614,72)~~#@select()how(),mul(324,924)~#'-;}?/mul(679,438)#@how()!mul(261,94)where()#~~mul(531,950),select()/+^>mul(387,794)$:{who()%!}mul(732,253)&why()mul(464,965)/mul(970,460) select()~mul(130,649)#mul(594,237)mul(715,98)mul(949,543)mul(341,112)$:mul(802,786)'who()%(*[>why(){how()mul(991,224)%mul(187,549)mul(780,836)from()!';!{&[mul(650,459)/]];select()#$select()^mul(273}># what()[(how()(mul(520,113)mul(616,814)mul(759,564)#}, /+)$)/mul;:^%~@mul(716,211)%who()what(),($mul(347,498)*-*&&mul(46,914)+;])>?mul(119,463)?%*-^+-*mul(281,195)*}mul(286,732#@,@when()*&:*>mul(945,971)-?mul(187,286)}how()^(how()where()don't()mul(156,64)},[why()?-/ when()mul(113,825)?)select()&?when(){mul(13,265)?~^,#how()mul(66,66)[{$ @?^^*mul(132,363)*}',-)what()/mul(373,957)when()mul(319,15)-%select()([+<:what()do()/$;)-]:@why();mul(597,465)-from()who()?how()where(){mul(836,692);[?,+<)mul(635,341)^;;why()+!(]mul(279,997):*'(^? mul(823,590))*'mul(822,370)?'-][what()*mul(953,604)mul(535,413),when()${+~)mul(484,759)mul(871,854)* {]>how(712,670),mul(373,823)+when()*{],#mul(159,820)[#^when()%&when()mul(799,36)+mul(315,321)&,when()%-]mul(548,25)select()&{why()from(197,262)]#?mul(358,469);'}mul(281,747)#why()mul(190,780);-/where()>mul(353,297)*why()[select()mulwhen()why()mul(578,187)*(mul(414,374)^~{when()mul(203,987)* when() (-#mul(441,105)from()+/mul(358,845));:where()from(308,392)){mul(886,76)@mul(675,177)where()when()]mul(380,191)when()('from()(select():mul(430,140);/select()how()mul(619,859)%#mul(606,253)/@*mul(40,538)>*]mul(989,612)!mul(765how(273,470)[why(630,394)what()how()what()mul(290,587)what()''?select()>>mul(916,886):($#);}};mul(743,103)/?&#what(713,97) when()-don't()^mul(164,643)+%$'mul(615,169)select():from(203,20);:&<( mul(643,22)when()<mul(551,479)!  } from()when()how()mul(444,127)who()[what()when()mul(31,673)<what()>+}({+from()mul(864,803)what(143,291)why()?select()who(596,912),]&do()where(18,622)*what()mul mul(668,272):how() when()(([*(mul(490,117)how()^ &don't()what()@where(){&mul(431,252),##>^%mul(928,524)
mul(508,343)mul(14,808))/&</*<what(),>mul(247,330)@'{mul(759,514)%?[from()']why()<mul(438,583)how(66,742)who())@#from()(mul(278,317)-how()-',#{mul(25,446)mul(379,907)what()*:^[mul(216,783)/>what(886,988)-mul(777,371[^ ;,why()do()mul(378,492) ,+!)from(410,175)where()mul(799,619)mul]:{$select()@,;!mul(535,785)where()from()'$*who(740,392)where()(mul(140,345)}-who(823,81)}%mul(183,163)/mul(950,595)~{:%from()>from()when()why(86,638)mul(730,655$^{-}-,mul(938,511)$who(){}~'$mul(659,791)#^mul(339,535) %:@why()}mul(57,448)who()')mul(863,872)<(*select()why() ),mul(121,155) '~~~select()how()when()$%mul(732>^;select()mul(913,601)]select()]@when()[(>>mul(697,273)} /don't()%mul(757,595);$@$>where())mul(818,23)>$~mul(649,864),{how() ;*[where():mul(342,657)^~select()]-mul(694,462)%]what()<};mul(697,789)who()[<where()$($~how(61,155)don't()why()select()select()mul(630,85)how()/' %select()why()&@do()+?@]^[when()?}mul(491,525)what()who()($[mul(166,732)'mul(343,571)when()$$when())~^from()-mul(662,576)<why(),who()%}mul(387,826)mul(363,451)*'mul(393,911);:,{mul(409,260@mul(235,163)%&from();-;-?/mul(107,703)^'^{-,,mul(503}mul(834,654)('~$mul(232,808) !!'/[who()]mul(140,807)*]mul(65,22)mulwhat()what()mul(501,321)/@ +[~from()mul(105,411)@?:from()mul(547,765['mul(94,110)what()mul(926,669)%!@mul(143,338) mul(835,604)<select()'&(where()what()$<!mul(906,637) <;%select()@}:%mul(725,278)<where()@who()<@when(501,813)mul(14,283)[;don't()why()+](select(172,592)*when()from()>mul(578,235)who()mul(186,328)mul(554,733)!>#){;@mul(138,929)mul(25,167)from()]%from(417,645)(when()where() ',mul(361,155+mul(203,77)where()];mul(839-<mul(216,980)what(714,198)mul(195,953)+:-:$}where()]![mul(481,558)<-~*]where()',}mul(405,524)?/?why(262,89)why()'mul(486,66)'#/-when()*>+mul(328,209)? when():*mul(686,850)-mul(604,374) ?#mul(68,860)>?(%^],select(709,64)?when()mul(548,321),) !>how()select()mul :+>how()%^@~@%mul(276,13)mul(692,25)$]from()/> mul(366,243)'*~}#<where()mul(651,369)who()@,what()when()why(743,252)where()<mul(900,577)#!#why(71,704)>select(){mul(691,190)!how()!#}~%do()<+what()&'~,+)mul(370,234) from(),mul(712,897){$#mul(579,611))/['mul(840,718){<from()what()#mul(53,189)^@mul(438,763)@,);~ #mul(967,224%%(^@,^&( :don't()^how()who()->&^(mul(641,81))*how(934,603)[&'how()^*mul(938,975)],{:%mul(747,483)#/}$^~why()mul(817,775)%}select() mul(273,96)mul(734,544)([##-where()mul(541,401$(}{,select()why(66,543)!mul(997,378)>how()!!;+))[mul(857,880)when()where()where()how(417,939)how(): mul(22where()/mul(938,942)>[[>how(), mul(552,726)mul(139what()from()why()mul(753,798)why()['+!!where(332,736)where():mul(465,371){}from(){]}[?where(860,482)mul(423,396)mul(677,239);^<]{{#what()mul(341,814)from()]-$#]^{ don't(){;who()}$mul(303,894){mul(100,628)]mul(481,789)^,mul(420,12)#]select()@&do()(mul(866,134)why();when(73,8)where()mul(971,422)mul(425,573)do()what()@{}<why()>#*mul(794,302(~from()%mul(777,75)-%%[when()select()mul(22,151),when()mul(983,529){?from()+[)*[what()[mul(4,608)mul(979,779@>what()$mul(138,543)mul(646,239)'mul(393-who()&!%who()where()mul(986,403)$],)<[mul(981,986)how()(select()!]mul(744,394)when()!*from()&why()how()mul(93,727)$what(506,382)[?~(who()<$mul(424,603)[&}?&why()]%mul(297,910);who(702,130)}select()who()(> +mul(860,951)$when()*who()[#+mul(431,588)how()+/:(%)>from()mul(8,460)(/:@!why()+mul(556,634)@?how()}@}}/^!mul(588,147)from()&when()@mul(499,831)
};who()!why(300,973);[@};mul(475,184)'+what()'how()mul(696,749)how()from()what()mul(294,115)@who()#[mul(409,579)?select(94,587)%)-~mul(541,209)^who()from()-who()}(-mul(612,20){[why()+* $#from():mul(727,495)/>mul(174,985)mul(563?%[how())(from()${;,mul(420,889){mul(350,415)%&??>{mul(408,995)(% $(why()+who()**mul(460,467),@}#(^*~>)mul(133,225)&/mul(587,173);/mul>+why()<,@-~[~!mul(622,327)$]how() @where()who():mul+*~-$>/ (where()mul(684,509)^,why()}<who()mul(143,815)where()what()]why()who()%}(from()mul(672,193?;:*where():>$!mul(929,986)$+[]- $;mul(119,51)- who(){#)mul(50,335)*;>?^select()<-mul(660,21)when(),<?/mul(330,503)^mul(597,951)?~({mul(936,959)from()(>mul(585,125)(mul(583,688){/why()>why()!mul-)^where(317,550)]when();who(877,651)!mul(77,828)~^@from()where()mul(901,180)%from();what()())mul(359,130)mul(732,295)>:<<^mul(686,46)from(){:</##%mul(774,846[& {how(597,615)/mul(27,581)why()(>?mul(744#(}how()from()#'when()'mul(375,566)*mul(177,377),what(205,216)mul<!~{'@}%&@>mul(511,997)}?how()where() select(294,364)how()]mul(975,7!-how()&?)mul(21,362)^&from()~mul(718,676)mul(87,923)}{,mul(988,768)]*<)?(!mul(198,629)#'/+select()mul(330,891);:where(701,805)where()/*<mul(810,309)select()^mul+('$,%-:mul(950,136)!/how()'*+@$mul(669,771)what(422,189);>/&)>mul(745,580*#mul(70,983)-who()#how())mul(283,218)mul(920,709)[from()^mul(447,444)<)]?select()mul(514,715)why()^from()({mul(616,746) #&how()where()>&&mul(819,532)+select()+mul(203,692)%(:(]+^#mul(75,664)}?%mul(506,516)'mul(967,985),mul(252,812)$'&+@!mul(69,311)!-}'~![mul(748,794):why()^>who()/mul(355,985)#?#mul(150,45)%{/[mul(249,182)##don't(),'?$~how()';mul(286,220)(~}:[:mul(297,443)mul(35,888)^@@from()why() $;;mul(417,143)when()mul(910,829)@mul(715,749) {~where()#*,/,mul(565,17);->mul(576,280)what()[from()[@mul(184,166)from(401,94) mul(569,96)-%{#{*when()mul(994,408)@}*mul(443,685)<-]}~mul(372,670)@};mul(376,74)mul(457,809))]mul(442,658)>)mul(503,506)mul(66,112)how()+!how()]select()who())mul(87,307)+[(mul(621,443)(:how()+:>don't()why()!how()>how()mul(674,840)<when(),]mul(921,417)+-don't()$;#]where()']-when()mul(221,145)when()select()from()(-'![how()mul(777,637);{+from()mul(945,14)#+mul(816,83)mul(195,318)@>what()mul(795,512)-<select())<+@@#mul(42,828)!$*?^^mul(234,453)[ from()@-:mul(885,717){why(49,248)@&'when()where()what()mul(91,905)mul(437,42)#}don't()mul(422,930),>mul(401,54)+/@+{!mul(691,931)?what()++[when()mul(442,585))~when()+-when()'select()mul(842,765)mul(62,265)(&who();who()(when()mul(194,966)*(,?mul(894,538)what()+]@&? mul(183,809); >%~what(840,775)who();when()mul(660,286)mul(355,338)>from()where()-$/from();,)mul(562,357)how()^mul(897,481)]<mul(289,64)how()<-~ where()how()where()mul(251,282)%when()where()when() *mul(291,786)select():when())who()(&mul(903,286)}:who()?mul(409,717)[how(),<when()@>why()mul(75,576)when()$]from()mul(509,51)(why()mul(910,246)/mul(325,400)+,+/[who()mul(303,777)when()&)/when():,/,mul(701,365)why():&mul(789,577)<&mul(620,292)what()@*-{";
}
