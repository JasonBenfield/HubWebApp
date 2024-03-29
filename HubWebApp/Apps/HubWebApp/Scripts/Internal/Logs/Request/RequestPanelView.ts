﻿import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
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

export class RequestPanelView extends GridView {

    readonly alert: MessageAlertView;
    readonly appKey: FormGroupTextView;
    readonly versionKey: BasicTextComponentView;
    readonly versionStatus: BasicTextComponentView;
    readonly userName: FormGroupTextView;
    readonly currentInstallation: FormGroupTextView;
    readonly timeRange: FormGroupTextView;
    readonly path: FormGroupTextView;
    readonly installationLink: TextLinkView;
    readonly logEntriesLink: TextLinkView;
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
        this.currentInstallation = gridContainer.addFormGroup(FormGroupTextView);
        this.currentInstallation.caption.setText('Installation');
        this.timeRange = gridContainer.addFormGroup(FormGroupTextView);
        this.timeRange.caption.setText('Time Range');
        this.path = gridContainer.addFormGroup(FormGroupTextView);
        this.path.caption.setText('Path');
        const nav = mainContent.addView(NavView);
        nav.pills();
        nav.setFlexCss(new FlexCss().column());
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

    showCurrentInstallation() { this.currentInstallation.show(); }

    hideCurrentInstallation() { this.currentInstallation.hide(); }
}