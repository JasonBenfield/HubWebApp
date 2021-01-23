import { Alert } from "XtiShared/Alert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceComponentViewModel } from "./ResourceComponentViewModel";
import { ResourceResultType } from '../../../Hub/Api/ResourceResultType';

export class ResourceComponent {
    constructor(
        private readonly vm: ResourceComponentViewModel,
        private readonly hubApi: HubAppApi
    ) {
    }

    private resourceID: number;

    setResourceID(resourceID: number) {
        this.resourceID = resourceID;
        this.vm.resourceName('');
    }

    private readonly alert = new Alert(this.vm.alert);

    async refresh() {
        let resource = await this.getResource(this.resourceID);
        this.vm.resourceName(resource.Name);
        this.vm.isAnonymousAllowed(resource.IsAnonymousAllowed);
        let resultType = ResourceResultType.values.value(resource.ResultType.Value);
        let resultTypeText: string;
        if (
            resultType.equalsAny(ResourceResultType.values.None, ResourceResultType.values.Json)
        ) {
            resultTypeText = '';
        }
        else {
            resultTypeText = resultType.DisplayText;
        }
        this.vm.resultType(resultTypeText);
    }

    private async getResource(resourceID: number) {
        let resource: IResourceModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                resource = await this.hubApi.Resource.GetResource(resourceID);
            }
        );
        return resource;
    }
}