import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BasicTextComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicTextComponentView";
import { CardAlertView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonCommandView, LinkCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { TextPreView } from "@jasonbenfield/sharedwebapp/Views/TextPreView";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../../HubTheme";
import { AppCardView } from "./AppCardView";
import { ModifierCategoryListCardView } from "./ModifierCategoryListCardView";
import { MostRecentErrorEventListCardView } from "./MostRecentErrorEventListCardView";
import { MostRecentRequestListCardView } from "./MostRecentRequestListCardView";
import { ResourceGroupListCardView } from "./ResourceGroupListCardView";
import { ButtonContainerView } from "@jasonbenfield/sharedwebapp/Views/ButtonContainerView";
import { InstallConfigurationListCardView } from "./InstallConfigurationListCardView";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";

export class AppDetailPanelView extends GridView {
    readonly alertView: MessageAlertView;
    readonly appCardView: AppCardView;
    readonly installConfigurationListCardView: InstallConfigurationListCardView;
    readonly repositoryLinkView: LinkCommandView;
    readonly refreshPublishedVersionsButton: ButtonCommandView;
    readonly installCurrentVersionButton: ButtonCommandView;
    private readonly appOptionsCardView: CardView;
    readonly appOptionsAlertView: CardAlertView;
    readonly appOptionsTextView: BasicTextComponentView;
    private readonly optionsCardView: CardView;
    readonly optionsAlertView: CardAlertView;
    readonly optionsTextView: BasicTextComponentView;
    readonly resourceGroupListCard: ResourceGroupListCardView;
    readonly modifierCategoryListCard: ModifierCategoryListCardView;
    readonly mostRecentRequestListCard: MostRecentRequestListCardView;
    readonly mostRecentErrorEventListCard: MostRecentErrorEventListCardView;

    readonly backButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.styleAsLayout();
        this.setTemplateRows(CssLengthUnit.flex(1));
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alertView = mainContent.addView(MessageAlertView);
        this.appCardView = mainContent.addView(AppCardView);
        this.appCardView.setMargin(MarginCss.bottom(3));
        this.installConfigurationListCardView = mainContent.addView(InstallConfigurationListCardView);
        this.installConfigurationListCardView.setMargin(MarginCss.bottom(3));
        const buttonContainerView = mainContent.addView(ButtonContainerView);
        buttonContainerView.setMargin(MarginCss.bottom(3));
        this.repositoryLinkView = buttonContainerView.addLinkCommand();
        this.repositoryLinkView.icon.solidStyle("github");
        this.refreshPublishedVersionsButton = buttonContainerView.addButtonCommand();
        this.refreshPublishedVersionsButton.icon.solidStyle("arrows-spin");
        this.installCurrentVersionButton = buttonContainerView.addButtonCommand();
        this.installCurrentVersionButton.icon.solidStyle("cloud-arrow-down");
        this.appOptionsCardView = mainContent.addView(CardView);
        this.appOptionsCardView.setMargin(MarginCss.bottom(3));
        this.appOptionsCardView.addCardTitleHeader().setText("Default App Options");
        this.appOptionsAlertView = this.appOptionsCardView.addCardAlert();
        const appOptionsBodyView = this.appOptionsCardView.addCardBody();
        this.appOptionsTextView = appOptionsBodyView.addView(TextPreView);
        this.optionsCardView = mainContent.addView(CardView);
        this.optionsCardView.setMargin(MarginCss.bottom(3));
        this.optionsCardView.addCardTitleHeader().setText("Default Shared Options");
        this.optionsAlertView = this.optionsCardView.addCardAlert();
        const optionsBodyView = this.optionsCardView.addCardBody();
        this.optionsTextView = optionsBodyView.addView(TextPreView);
        this.resourceGroupListCard = mainContent.addView(ResourceGroupListCardView)
        this.resourceGroupListCard.setMargin(MarginCss.bottom(3));
        this.modifierCategoryListCard = mainContent.addView(ModifierCategoryListCardView)
        this.modifierCategoryListCard.setMargin(MarginCss.bottom(3));
        this.mostRecentRequestListCard = mainContent.addView(MostRecentRequestListCardView)
        this.mostRecentRequestListCard.setMargin(MarginCss.bottom(3));
        this.mostRecentErrorEventListCard = mainContent.addView(MostRecentErrorEventListCardView)
        this.mostRecentErrorEventListCard.setMargin(MarginCss.bottom(3));

        const toolbar = this.addView(ToolbarView);
        HubTheme.instance.commandToolbar.toolbar(toolbar);
        this.backButton = toolbar.columnStart.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.backButton(this.backButton);
    }

    showAppOptions() {
        this.appOptionsCardView.show();
    }

    hideAppOptions() {
        this.appOptionsCardView.hide();
    }

    showOptions() {
        this.optionsCardView.show();
    }

    hideOptions() {
        this.optionsCardView.hide();
    }
}