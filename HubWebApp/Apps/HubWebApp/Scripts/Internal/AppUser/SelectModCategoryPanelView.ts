import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { ButtonListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { NavView } from "@jasonbenfield/sharedwebapp/Views/NavView";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../HubTheme";
import { ModCategoryButtonListItemView } from "./ModCategoryButtonListItemView";

export class SelectModCategoryPanelView extends GridView {
    readonly defaultModButton: ButtonCommandView;
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly modCategories: ButtonListGroupView<ModCategoryButtonListItemView>;
    readonly backButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.styleAsLayout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.setViewName(SelectModCategoryPanelView.name);
        const defaultModList = mainContent.addView(NavView);
        defaultModList.pills();
        defaultModList.setMargin(MarginCss.bottom(3));
        this.defaultModButton = defaultModList.addButtonCommand();
        this.defaultModButton.setTextCss(new TextCss().start());
        this.defaultModButton.setText('Default Modifier');
        const card = mainContent.addView(CardView);
        this.titleHeader = card.addCardTitleHeader();
        this.alert = card.addCardAlert();
        this.modCategories = card.addButtonListGroup(ModCategoryButtonListItemView);
        card.setMargin(MarginCss.bottom(3));
        const toolbar = this.addView(ToolbarView);
        HubTheme.instance.commandToolbar.toolbar(toolbar);
        this.backButton = toolbar.columnStart.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.backButton(this.backButton);
    }
}