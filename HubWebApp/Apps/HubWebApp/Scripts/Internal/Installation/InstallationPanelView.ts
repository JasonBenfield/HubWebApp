import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { FlexCss } from "@jasonbenfield/sharedwebapp/FlexCss";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BasicTextComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicTextComponentView";
import { BlockView } from "@jasonbenfield/sharedwebapp/Views/BlockView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { FormGroupGridView, FormGroupTextView, FormGroupView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
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
    readonly deleteButton: ButtonCommandView;
    readonly logEntriesLink: TextLinkView;
    readonly requestsLink: TextLinkView;
    readonly menuButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.layout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alert = mainContent.addView(MessageAlertView);
        this.confirm = mainContent.addView(ModalConfirmView);
        const gridContainer = mainContent.addView(FormGroupGridView);
        this.appKey = gridContainer.addFormGroup(FormGroupTextView);
        this.appKey.caption.setText('App');
        const versionFormGroup = gridContainer.addFormGroup(FormGroupView);
        versionFormGroup.caption.setText('Version');
        const versionBlock = versionFormGroup.valueCell.addView(BlockView);
        versionBlock.addCssName('form-control-plaintext');
        this.versionKey = versionBlock.addView(TextSpanView);
        this.versionKey.setMargin(MarginCss.end(3));
        this.versionStatus = versionBlock.addView(TextSpanView);
        this.installationStatus = gridContainer.addFormGroup(FormGroupTextView);
        this.installationStatus.caption.setText('Installation Status');
        const locationFormGroup = gridContainer.addFormGroup(FormGroupView);
        locationFormGroup.caption.setText('Location');
        const locationBlock = locationFormGroup.valueCell.addView(BlockView);
        locationBlock.addCssName('form-control-plaintext');
        this.location = locationBlock.addView(TextSpanView);
        this.location.setMargin(MarginCss.end(3));
        this.current = locationBlock.addView(TextSpanView);
        this.domain = gridContainer.addFormGroup(FormGroupTextView);
        this.domain.caption.setText('Domain');
        this.siteName = gridContainer.addFormGroup(FormGroupTextView);
        this.siteName.caption.setText('Site Name');
        const nav = mainContent.addView(NavView);
        nav.pills();
        nav.setFlexCss(new FlexCss().column());
        this.deleteButton = nav.addButtonCommand();
        this.deleteButton.setTextCss(new TextCss().start());
        this.deleteButton.icon.solidStyle('times');
        this.deleteButton.setText('Delete');
        this.logEntriesLink = nav.addTextLink();
        this.logEntriesLink.setText('View Log Entries');
        this.requestsLink = nav.addTextLink();
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
}