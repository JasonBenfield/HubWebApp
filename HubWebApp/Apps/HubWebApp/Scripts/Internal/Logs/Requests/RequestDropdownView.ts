﻿import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { DropdownComponentView } from "@jasonbenfield/sharedwebapp/Views/Dropdown";
import { GridCellView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { TextLinkView } from "@jasonbenfield/sharedwebapp/Views/TextLinkView";

export class RequestDropdownView extends GridCellView {
    readonly logEntryLink: TextLinkView;

    constructor(container: BasicComponentView) {
        super(container);
        const dropdown = this.addView(DropdownComponentView);
        dropdown.button.addView(TextSpanView).setText('View');
        dropdown.button.useOutlineStyle(ContextualClass.secondary);
        const requestDropdownLink = dropdown.menu.addTextLinkItem();
        requestDropdownLink.link.setText('Log Entries');
        this.logEntryLink = requestDropdownLink.link;
    }
}