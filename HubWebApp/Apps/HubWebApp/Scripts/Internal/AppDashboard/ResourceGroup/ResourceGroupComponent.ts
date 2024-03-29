﻿import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ResourceGroupComponentView } from "./ResourceGroupComponentView";

export class ResourceGroupComponent {
    private groupID: number;

    private readonly alert: MessageAlert;
    private readonly groupName: TextComponent;

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: ResourceGroupComponentView
    ) {
        new TextComponent(view.titleHeader).setText('Resource Group');
        this.alert = new CardAlert(view.alert).alert;
        this.groupName = new TextComponent(view.groupName);
        this.view.hideAnonMessage();
    }

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    async refresh() {
        const group = await this.getResourceGroup(this.groupID);
        this.groupName.setText(group.Name.DisplayText);
        if (group.IsAnonymousAllowed) {
            this.view.showAnonMessage();
        }
        else {
            this.view.hideAnonMessage();
        }
    }

    private getResourceGroup(groupID: number) {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubClient.ResourceGroup.GetResourceGroup({
                VersionKey: 'Current',
                GroupID: groupID
            })
        );
    }
}