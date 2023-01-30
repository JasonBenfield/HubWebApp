import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { FlexCss } from "@jasonbenfield/sharedwebapp/FlexCss";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { PaddingCss } from "@jasonbenfield/sharedwebapp/PaddingCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BasicTextComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicTextComponentView";
import { BlockView } from "@jasonbenfield/sharedwebapp/Views/BlockView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { FormGroupGridView, FormGroupTextView, FormGroupView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { NavView } from "@jasonbenfield/sharedwebapp/Views/NavView";
import { TextLinkView } from "@jasonbenfield/sharedwebapp/Views/TextLinkView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../../HubTheme";

export class LogEntryPanelView extends GridView {

    readonly alert: MessageAlertView;
    readonly appKey: FormGroupTextView;
    readonly versionKey: BasicTextComponentView;
    readonly versionStatus: BasicTextComponentView;
    readonly userName: FormGroupTextView;
    readonly path: FormGroupTextView;
    readonly timeOccurred: FormGroupTextView;
    readonly severity: FormGroupTextView;
    readonly caption: FormGroupTextView;
    readonly message: FormGroupTextView;
    readonly detail: FormGroupTextView;
    readonly sourceLogEntryLink: TextLinkView;
    readonly targetLogEntryLink: TextLinkView;
    readonly requestLink: TextLinkView;
    readonly installationLink: TextLinkView;
    readonly menuButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.layout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alert = mainContent.addView(MessageAlertView);
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
        this.userName = gridContainer.addFormGroup(FormGroupTextView);
        this.userName.caption.setText('User Name');
        this.path = gridContainer.addFormGroup(FormGroupTextView);
        this.path.caption.setText('Path');
        this.timeOccurred = gridContainer.addFormGroup(FormGroupTextView);
        this.timeOccurred.caption.setText('Time Occurred');
        this.severity = gridContainer.addFormGroup(FormGroupTextView);
        this.severity.caption.setText('Severity');
        this.caption = gridContainer.addFormGroup(FormGroupTextView);
        this.caption.caption.setText('Caption');
        this.message = gridContainer.addFormGroup(FormGroupTextView);
        this.message.caption.setText('Message');
        this.detail = gridContainer.addFormGroup(FormGroupTextView);
        this.detail.caption.setText('Details');
        const nav = mainContent.addView(NavView);
        nav.pills();
        nav.setFlexCss(new FlexCss().column());
        this.sourceLogEntryLink = nav.addTextLink();
        this.sourceLogEntryLink.setText('Source Log Entry');
        this.targetLogEntryLink = nav.addTextLink();
        this.targetLogEntryLink.setText('Target Log Entry');
        this.requestLink = nav.addTextLink();
        this.requestLink.setText('View Request');
        this.installationLink = nav.addTextLink();
        this.installationLink.setText('View Installation');
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.menuButton = HubTheme.instance.commandToolbar.menuButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
    }

    showCaption() { this.caption.show(); }

    hideCaption() { this.caption.hide(); }

    showMessage() { this.message.show(); }

    hideMessage() { this.message.hide(); }

    showDetail() { this.detail.show(); }

    hideDetail() { this.detail.hide(); }
}