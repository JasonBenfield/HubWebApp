import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { FlexCss } from "@jasonbenfield/sharedwebapp/FlexCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { FormGroupTextView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { FormGroupContainerView } from "@jasonbenfield/sharedwebapp/Views/FormGroupContainerView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { NavView } from "@jasonbenfield/sharedwebapp/Views/NavView";
import { TextLinkView } from "@jasonbenfield/sharedwebapp/Views/TextLinkView";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../../HubTheme";

export class SessionPanelView extends GridView {

    readonly alert: MessageAlertView;
    readonly timeRangeTextView: FormGroupTextView;
    readonly userNameFormGroupView: FormGroupTextView;
    readonly remoteAddressFormGroupView: FormGroupTextView;
    readonly userAgentFormGroupView: FormGroupTextView;
    readonly userLink: TextLinkView;
    readonly requestsLink: TextLinkView;
    readonly menuButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.styleAsLayout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alert = mainContent.addView(MessageAlertView);
        const gridContainer = mainContent.addView(FormGroupContainerView);
        this.timeRangeTextView = gridContainer.addFormGroup(FormGroupTextView);
        this.timeRangeTextView.caption.setText('Time Range');
        this.userNameFormGroupView = gridContainer.addFormGroup(FormGroupTextView);
        this.userNameFormGroupView.caption.setText('User Name');
        this.remoteAddressFormGroupView = gridContainer.addFormGroup(FormGroupTextView);
        this.remoteAddressFormGroupView.caption.setText('Remote Address');
        this.userAgentFormGroupView = gridContainer.addFormGroup(FormGroupTextView);
        this.userAgentFormGroupView.caption.setText('User Agent');
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
}