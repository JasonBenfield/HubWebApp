import { CardAlert } from "@jasonbenfield/sharedwebapp/Card/CardAlert";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceResultType } from '../../../Hub/Api/ResourceResultType';
import { ResourceComponentView } from "./ResourceComponentView";

export class ResourceComponent {
    private readonly alert: MessageAlert;
    private resourceID: number;
    private readonly resourceName: TextBlock;
    private readonly resultType: TextBlock;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ResourceComponentView
    ) {
        new TextBlock('Resource', this.view.titleHeader);
        this.alert = new CardAlert(this.view.alert).alert;
        this.resourceName = new TextBlock('', view.resourceName);
        this.resultType = new TextBlock('', view.resultType);
    }

    setResourceID(resourceID: number) {
        this.resourceID = resourceID;
        this.resourceName.setText('');
        this.resultType.setText('');
        this.view.hideAnon();
    }

    async refresh() {
        let resource = await this.getResource(this.resourceID);
        this.resourceName.setText(resource.Name);
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
        this.resultType.setText(resultTypeText);
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