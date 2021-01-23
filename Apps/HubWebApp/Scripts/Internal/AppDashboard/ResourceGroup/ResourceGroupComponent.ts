import { Alert } from "XtiShared/Alert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceGroupComponentViewModel } from "./ResourceGroupComponentViewModel";

export class ResourceGroupComponent {
    constructor(
        private readonly vm: ResourceGroupComponentViewModel,
        private readonly hubApi: HubAppApi
    ) {
    }

    private groupID: number;

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    private readonly alert = new Alert(this.vm.alert);

    async refresh() {
        let group = await this.getResourceGroup(this.groupID);
        this.vm.groupName(group.Name);
        this.vm.isAnonymousAllowed(group.IsAnonymousAllowed);
    }

    private async getResourceGroup(groupID: number) {
        let group: IResourceGroupModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                group = await this.hubApi.ResourceGroup.GetResourceGroup(groupID);
            }
        );
        return group;
    }
}