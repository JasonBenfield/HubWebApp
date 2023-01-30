import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { FlexCss } from "@jasonbenfield/sharedwebapp/FlexCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { FormGroupGridView, FormGroupTextView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { NavView } from "@jasonbenfield/sharedwebapp/Views/NavView";
import { TextLinkView } from "@jasonbenfield/sharedwebapp/Views/TextLinkView";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../../HubTheme";

export class SessionPanelView extends GridView {

    readonly alert: MessageAlertView;
    readonly userName: FormGroupTextView;
    readonly remoteAddress: FormGroupTextView;
    readonly userAgent: FormGroupTextView;
    readonly userLink: TextLinkView;
    readonly requestsLink: TextLinkView;
    readonly menuButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.layout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alert = mainContent.addView(MessageAlertView);
        const gridContainer = mainContent.addView(FormGroupGridView);
        this.userName = gridContainer.addFormGroup(FormGroupTextView);
        this.userName.caption.setText('User Name');
        const nav = mainContent.addView(NavView);
        nav.pills();
        nav.setFlexCss(new FlexCss().column());
        this.userLink = nav.addTextLink();
        this.userLink.setText('View User');
        this.requestsLink = nav.addTextLink();
        this.requestsLink.setText('View Requests');
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.menuButton = HubTheme.instance.commandToolbar.menuButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
    }

    showUserAgent() { this.userAgent.show(); }

    hideUserAgent() { this.userAgent.hide(); }

    showRemoteAddress() { this.remoteAddress.show(); }

    hideRemoteAddress() { this.remoteAddress.hide(); }
}