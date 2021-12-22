import { CardTitleHeader } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeader";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceResultType } from '../../../Hub/Api/ResourceResultType';
import { ResourceComponentView } from "./ResourceComponentView";

export class ResourceComponent {
    private readonly alert: MessageAlert;
    private resourceID: number;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ResourceComponentView
    ) {
        new CardTitleHeader('Resource', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
    }

    setResourceID(resourceID: number) {
        this.resourceID = resourceID;
        this.view.setResourceName('');
        this.view.setResultType('');
        this.view.hideAnon();
    }

    async refresh() {
        let resource = await this.getResource(this.resourceID);
        this.view.setResourceName(resource.Name);
        if (resource.IsAnonymousAllowed) {
            this.view.showAnon();
        }
        else {
            this.view.hideAnon();
        }
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
        this.view.setResultType(resultTypeText);
    }

    private async getResource(resourceID: number) {
        let resource: IResourceModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                resource = await this.hubApi.Resource.GetResource({
                    VersionKey: 'Current',
                    ResourceID: resourceID
                });
            }
        );
        return resource;
    }
}