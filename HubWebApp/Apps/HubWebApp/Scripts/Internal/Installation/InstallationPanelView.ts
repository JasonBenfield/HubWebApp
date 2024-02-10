import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { FlexCss } from "@jasonbenfield/sharedwebapp/FlexCss";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BasicTextComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicTextComponentView";
import { BlockView } from "@jasonbenfield/sharedwebapp/Views/BlockView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { FormGroupTextView, FormGroupView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { FormGroupContainerView } from "@jasonbenfield/sharedwebapp/Views/FormGroupContainerView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { ModalConfirmView } from "@jasonbenfield/sharedwebapp/Views/Modal";
import { NavView } from "@jasonbenfield/sharedwebapp/Views/NavView";
import { TextLinkView } from "@jasonbenfield/sharedwebapp/Views/TextLinkView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../HubTheme";

export class InstallationPanelView extends GridView {
    readonly alert: MessageAlertView;
    readonly confirm: ModalConfirmView;
    readonly appKey: FormGroupTextView;
    readonly versionKey: BasicTextComponentView;
    readonly versionStatus: BasicTextComponentView;
    readonly installationStatus: FormGroupTextView;
    readonly location: BasicTextComponentView;
    readonly current: BasicTextComponentView;
    readonly domain: FormGroupTextView;
    readonly siteName: FormGroupTextView;
    readonly mostRecentRequest: FormGroupTextView;
    readonly deleteButton: ButtonCommandView;
    readonly appLink: TextLinkView;
    readonly logEntriesLink: TextLinkView;
    readonly requestsLink: TextLinkView;
    readonly menuButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.styleAsLayout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alert = mainContent.addView(MessageAlertView);
        this.confirm = mainContent.addView(ModalConfirmView);
        const gridContainer = mainContent.addView(FormGroupContainerView);
        this.appKey = gridContainer.addFormGroup(FormGroupTextView);
        this.appKey.caption.setText('App');
        const versionFormGroup = gridContainer.addFormGroup(FormGroupView);
        versionFormGroup.caption.setText('Version');
        const versionBlock = versionFormGroup.valueCell.addView(BlockView);
        versionBlock.addCssName('form-control-plaintext');
        this.versionKey = versionBlock.addView(TextSpanView);
        this.versionKey.setMargin(MarginCss.end(3));
        this.versionStatus = versionBlock.addView(TextSpanView);
        this.installationStatus = gridContainer.addFormGroupTextView();
        this.installationStatus.caption.setText('Installation Status');
        const locationFormGroup = gridContainer.addFormGroup(FormGroupView);
        locationFormGroup.caption.setText('Location');
        const locationBlock = locationFormGroup.valueCell.addView(BlockView);
        locationBlock.addCssName('form-control-plaintext');
        this.location = locationBlock.addView(TextSpanView);
        this.location.setMargin(MarginCss.end(3));
        this.current = locationBlock.addView(TextSpanView);
        this.domain = gridContainer.addFormGroupTextView();
        this.domain.caption.setText('Domain');
        this.siteName = gridContainer.addFormGroupTextView();
        this.siteName.caption.setText('Site Name');
        this.mostRecentRequest = gridContainer.addFormGroupTextView();
        this.mostRecentRequest.caption.setText('Latest Request');
        this.deleteButton = mainContent.addView(ButtonCommandView);
        this.deleteButton.setTextCss(new TextCss().start());
        this.deleteButton.icon.solidStyle('times');
        this.deleteButton.setText('Delete');
        this.deleteButton.useOutlineStyle(ContextualClass.danger);
        this.deleteButton.setMargin(MarginCss.xs({ bottom: 3, start: 3 }));
        const linkNav = mainContent.addView(NavView);
        linkNav.pills();
        linkNav.setFlexCss(new FlexCss().column());
        this.appLink = linkNav.addTextLink();
        this.appLink.setText('View App');
        this.logEntriesLink = linkNav.addTextLink();
        this.logEntriesLink.setText('View Log Entries');
        this.requestsLink = linkNav.addTextLink();
        this.requestsLink.setText('View Requests');
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.menuButton = HubTheme.instance.commandToolbar.menuButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
    }

    showDomain() {
        this.domain.show();
    }

    hideDomain() {
        this.domain.hide();
    }

    showSiteName() {
        this.siteName.show();
    }

    hideSiteName() {
        this.siteName.hide();
    }

    showMostRecentRequest() { this.mostRecentRequest.show(); }

    hideMostRecentRequest() { this.mostRecentRequest.hide(); }
}