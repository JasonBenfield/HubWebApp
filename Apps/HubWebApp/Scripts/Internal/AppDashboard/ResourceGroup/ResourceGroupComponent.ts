import { CardTitleHeader } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeader";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceGroupComponentView } from "./ResourceGroupComponentView";

export class ResourceGroupComponent {
    private groupID: number;

    private readonly alert: MessageAlert;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ResourceGroupComponentView
    ) {
        new CardTitleHeader('Resource Group', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.view.hideAnonMessage();
    }

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    async refresh() {
        let group = await this.getResourceGroup(this.groupID);
        this.view.setGroupName(group.Name);
        if (group.IsAnonymousAllowed) {
            this.view.showAnonMessage();
        }
        else {
            this.view.hideAnonMessage();
        }
    }

    private async getResourceGroup(groupID: number) {
        let group: IResourceGroupModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                group = await this.hubApi.ResourceGroup.GetResourceGroup({
                    VersionKey: 'Current',
                    GroupID: groupID
                });
            }
        );
        return group;
    }
}