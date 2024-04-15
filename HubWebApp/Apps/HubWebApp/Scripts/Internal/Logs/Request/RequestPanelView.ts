import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { FlexCss } from "@jasonbenfield/sharedwebapp/FlexCss";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BasicTextComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicTextComponentView";
import { BlockView } from "@jasonbenfield/sharedwebapp/Views/BlockView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { FormGroupTextView, FormGroupView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { FormGroupContainerView } from "@jasonbenfield/sharedwebapp/Views/FormGroupContainerView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { NavView } from "@jasonbenfield/sharedwebapp/Views/NavView";
import { TextLinkView } from "@jasonbenfield/sharedwebapp/Views/TextLinkView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../../HubTheme";

export class RequestPanelView extends GridView {

    readonly alert: MessageAlertView;
    readonly appKeyFormGroupView: FormGroupTextView;
    readonly versionKeyFormGroupView: BasicTextComponentView;
    readonly versionStatusFormGroupView: BasicTextComponentView;
    readonly userNameFormGroupView: FormGroupTextView;
    readonly userAgentFormGroupView: FormGroupTextView;
    readonly remoteAddressFormGroupView: FormGroupTextView;
    readonly currentInstallationFormGroupView: FormGroupTextView;
    readonly timeRangeFormGroupView: FormGroupTextView;
    readonly pathFormGroupView: FormGroupTextView;
    readonly sessionLink: TextLinkView;
    readonly installationLink: TextLinkView;
    readonly logEntriesLink: TextLinkView;
    readonly sourceRequestLink: TextLinkView;
    readonly targetRequestLink: TextLinkView;
    readonly menuButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.styleAsLayout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alert = mainContent.addView(MessageAlertView);
        const gridContainer = mainContent.addView(FormGroupContainerView);
        this.appKeyFormGroupView = gridContainer.addFormGroupTextView();
        this.appKeyFormGroupView.caption.setText('App');
        const versionFormGroup = gridContainer.addFormGroup(FormGroupView);
        versionFormGroup.caption.setText('Version');
        const versionBlock = versionFormGroup.valueCell.addView(BlockView);
        versionBlock.addCssName('form-control-plaintext');
        this.versionKeyFormGroupView = versionBlock.addView(TextSpanView);
        this.versionKeyFormGroupView.setMargin(MarginCss.end(3));
        this.versionStatusFormGroupView = versionBlock.addView(TextSpanView);
        this.userNameFormGroupView = gridContainer.addFormGroupTextView();
        this.userNameFormGroupView.caption.setText('User Name');
        this.userAgentFormGroupView = gridContainer.addFormGroupTextView();
        this.remoteAddressFormGroupView = gridContainer.addFormGroupTextView();
        this.currentInstallationFormGroupView = gridContainer.addFormGroupTextView();
        this.currentInstallationFormGroupView.caption.setText('Installation');
        this.timeRangeFormGroupView = gridContainer.addFormGroupTextView();
        this.timeRangeFormGroupView.caption.setText('Time Range');
        this.pathFormGroupView = gridContainer.addFormGroupTextView();
        this.pathFormGroupView.caption.setText('Path');
        const nav = mainContent.addView(NavView);
        nav.pills();
        nav.setFlexCss(new FlexCss().column());
        this.sourceRequestLink = nav.addTextLink();
        this.sourceRequestLink.setText('Source Request');
        this.targetRequestLink = nav.addTextLink();
        this.targetRequestLink.setText('View Target Requests');
        this.sessionLink = nav.addTextLink();
        this.sessionLink.setText('View Session');
        this.installationLink = nav.addTextLink();
        this.installationLink.setText('View Installation');
        this.logEntriesLink = nav.addTextLink();
        this.logEntriesLink.setText('View Log Entries');
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.menuButton = HubTheme.instance.commandToolbar.menuButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
    }

    showCurrentInstallation() { this.currentInstallationFormGroupView.show(); }

    hideCurrentInstallation() { this.currentInstallationFormGroupView.hide(); }
}