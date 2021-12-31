import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceGroupComponentView } from "./ResourceGroupComponentView";

export class ResourceGroupComponent {
    private groupID: number;

    private readonly alert: MessageAlert;
    private readonly groupName: TextBlock;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ResourceGroupComponentView
    ) {
        new TextBlock('Resource Group', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.groupName = new TextBlock('', this.view.groupName);
        this.view.hideAnonMessage();
    }

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    async refresh() {
        let group = await this.getResourceGroup(this.groupID);
        this.groupName.setText(group.Name);
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