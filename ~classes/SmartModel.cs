using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public class SmartModel
	{

		/* properties */


		public SmartModel[] Categories { get; set; }
		public SmartModel[] Collections { get; set; }
		public SmartModel[] Groups { get; set; }
		public SmartModel[] Sections { get; set; }
		public SmartModel[] Parts { get; set; }
		public SmartModel[] Items { get; set; }
		public SmartModel[] Resources { get; set; }
		public SmartModel[] Links { get; set; }

		public int Id { get; set; }
		public string Key { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string FIO { get; set; }
		public string Sex { get; set; }
		public string Init { get; set; }
		public string Category { get; set; }
		public string Collection { get; set; }
		public string Group { get; set; }
		public string Section { get; set; }
		public string Part { get; set; }
		public string Item { get; set; }
		public string Resource { get; set; }
		public string Link { get; set; }
		public string Tel { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Place { get; set; }
		public string Note { get; set; }
		public string Target { get; set; }
		public string Owner { get; set; }
		public string Face { get; set; }
		public string Title { get; set; }
		public string Short { get; set; }
		public string Full { get; set; }
		public string Text { get; set; }
		public string Desc { get; set; }
		public string Info { get; set; }
		public string Old { get; set; }
		public string Src { get; set; }
		public string Img { get; set; }
		public string Image { get; set; }
		public string Photo { get; set; }
		public string Icon { get; set; }
		public string Css { get; set; }
		public string Style { get; set; }
		public string Type { get; set; }
		public string Mode { get; set; }
		public string Class { get; set; }
		public string Url { get; set; }
		public string Ref { get; set; }
		public string Href { get; set; }
		public string Inner { get; set; }
		public string Remark { get; set; }

		public string Data { get; set; }
		public string Data1 { get; set; }
		public string Data2 { get; set; }
		public string Data3 { get; set; }

		public int Val { get; set; }
		public int Val1 { get; set; }
		public int Val2 { get; set; }
		public int Val3 { get; set; }

		public string Value { get; set; }
		public string Value1 { get; set; }
		public string Value2 { get; set; }
		public string Value3 { get; set; }

		public string Date { get; set; }
		public string Date1 { get; set; }
		public string Date2 { get; set; }
		public string Date3 { get; set; }

		public string Set { get; set; }
		public string Set1 { get; set; }
		public string Set2 { get; set; }
		public string Set3 { get; set; }

		public string Props { get; set; }
		public string Tags { get; set; }

		public HtmlString GenFIOHtml { get; set; }
		public HtmlString GenInitHtml { get; set; }
		public HtmlString GenCategoryHtml { get; set; }
		public HtmlString GenCollectionHtml { get; set; }
		public HtmlString GenGroupHtml { get; set; }
		public HtmlString GenSectionHtml { get; set; }
		public HtmlString GenPartHtml { get; set; }
		public HtmlString GenItemHtml { get; set; }
		public HtmlString GenResourceHtml { get; set; }
		public HtmlString GenLinkHtml { get; set; }
		public HtmlString GenAddressHtml { get; set; }
		public HtmlString GenPlaceHtml { get; set; }
		public HtmlString GenNoteHtml { get; set; }
		public HtmlString GenTargetHtml { get; set; }
		public HtmlString GenOwnerHtml { get; set; }
		public HtmlString GenTitleHtml { get; set; }
		public HtmlString GenShortHtml { get; set; }
		public HtmlString GenFullHtml { get; set; }
		public HtmlString GenTextHtml { get; set; }
		public HtmlString GenDescHtml { get; set; }
		public HtmlString GenInfoHtml { get; set; }
		public HtmlString GenOldHtml { get; set; }
		public HtmlString GenInnerHtml { get; set; }

		public bool Hidden { get; set; }
		public bool Readonly { get; set; }
		public bool Disabled { get; set; }
		public bool Off { get; set; }
		public bool On { get; set; }


		public Func<string, HtmlString> ToFIOHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToInitHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToCategoryHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToCollectionHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToGroupHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToSectionHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToPartHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToItemHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToResourceHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToLinkHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToAddressHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToPlaceHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToNoteHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToTargetHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToOwnerHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToTitleHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToShortHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToFullHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToTextHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToDescHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToInfoHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToOldHtml { get; set; } = x => x.ToHtml(true);
		public Func<string, HtmlString> ToInnerHtml { get; set; } = x => x.ToHtml(true);


		/* readonly properties */


		public RegistryList Props2 => new(Props);
		public string[] Tags2 => Tags?.Split([',', ';']);

		public bool IsHidden => Hidden;
		public bool IsReadonly => Readonly;
		public bool IsDisabled => Disabled;
		public bool IsOff => Off;
		public bool IsOn => On;

		public bool HasCategories => Categories?.Length > 0;
		public bool HasCollections => Collections?.Length > 0;
		public bool HasGroups => Groups?.Length > 0;
		public bool HasSections => Sections?.Length > 0;
		public bool HasParts => Parts?.Length > 0;
		public bool HasItems => Items?.Length > 0;
		public bool HasResources => Resources?.Length > 0;
		public bool HasLinks => Links?.Length > 0;

		public HtmlString FIOHtml => GenFIOHtml ??= ToFIOHtml(FIO);
		public HtmlString InitHtml => GenInitHtml ??= ToInitHtml(Init);
		public HtmlString CategoryHtml => GenCategoryHtml ??= ToCategoryHtml(Category);
		public HtmlString CollectionHtml => GenCollectionHtml ??= ToCollectionHtml(Collection);
		public HtmlString GroupHtml => GenGroupHtml ??= ToGroupHtml(Group);
		public HtmlString SectionHtml => GenSectionHtml ??= ToSectionHtml(Section);
		public HtmlString PartHtml => GenPartHtml ??= ToPartHtml(Part);
		public HtmlString ItemHtml => GenItemHtml ??= ToItemHtml(Item);
		public HtmlString ResourceHtml => GenResourceHtml ??= ToResourceHtml(Resource);
		public HtmlString LinkHtml => GenLinkHtml ??= ToLinkHtml(Link);
		public HtmlString AddressHtml => GenAddressHtml ??= ToAddressHtml(Address);
		public HtmlString PlaceHtml => GenPlaceHtml ??= ToPlaceHtml(Place);
		public HtmlString NoteHtml => GenNoteHtml ??= ToNoteHtml(Note);
		public HtmlString TargetHtml => GenTargetHtml ??= ToTargetHtml(Target);
		public HtmlString OwnerHtml => GenOwnerHtml ??= ToOwnerHtml(Owner);
		public HtmlString TitleHtml => GenTitleHtml ??= ToTitleHtml(Title);
		public HtmlString ShortHtml => GenShortHtml ??= ToShortHtml(Short);
		public HtmlString FullHtml => GenFullHtml ??= ToFullHtml(Full);
		public HtmlString TextHtml => GenTextHtml ??= ToTextHtml(Text);
		public HtmlString DescHtml => GenDescHtml ??= ToDescHtml(Desc);
		public HtmlString InfoHtml => GenInfoHtml ??= ToInfoHtml(Info);
		public HtmlString OldHtml => GenOldHtml ??= ToOldHtml(Old);
		public HtmlString InnerHtml => GenInnerHtml ??= ToInnerHtml(Inner);

	}

}
