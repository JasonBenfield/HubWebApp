import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonListGroupView, ListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { TextBlockView } from "@jasonbenfield/sharedwebapp/Views/TextBlockView";
import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../HubTheme";
import { ModCategoryButtonListItemView } from "./ModCategoryButtonListItemView";

export class SelectModCategoryPanelView extends GridView {
    readonly defaultModList: ListGroupView;
    readonly defaultModClicked: IEventHandler<any>;
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly modCategories: ButtonListGroupView;
    readonly backButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.layout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.setViewName(SelectModCategoryPanelView.name);
        this.defaultModList = mainContent.addView(ListGroupView);
        this.defaultModList.setMargin(MarginCss.bottom(3));
        const defaultModListItem = this.defaultModList.addListGroupItem();
        defaultModListItem
            .addView(TextBlockView)
            .configure(tb => tb.setText('Default Modifier'));
        const card = mainContent.addView(CardView);
        this.titleHeader = card.addCardTitleHeader();
        this.alert = card.addCardAlert();
        this.modCategories = card.addView(ButtonListGroupView);
        this.modCategories.setItemViewType(ModCategoryButtonListItemView);
        card.setMargin(MarginCss.bottom(3));
        const toolbar = this.addView(ToolbarView);
        HubTheme.instance.commandToolbar.toolbar(toolbar);
        this.backButton = toolbar.columnStart.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.backButton(this.backButton);
    }
}