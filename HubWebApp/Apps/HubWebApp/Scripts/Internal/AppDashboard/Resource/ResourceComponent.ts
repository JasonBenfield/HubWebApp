﻿import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { ResourceResultType } from '../../../Lib/Api/ResourceResultType';
import { ResourceComponentView } from "./ResourceComponentView";

export class ResourceComponent {
    private readonly alert: MessageAlert;
    private resourceID: number;
    private readonly resourceName: TextComponent;
    private readonly resultType: TextComponent;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ResourceComponentView
    ) {
        new TextComponent(view.titleHeader).setText('Resource');
        this.alert = new CardAlert(view.alert).alert;
        this.resourceName = new TextComponent(view.resourceName);
        this.resultType = new TextComponent(view.resultType);
    }

    setResourceID(resourceID: number) {
        this.resourceID = resourceID;
        this.resourceName.setText('');
        this.resultType.setText('');
        this.view.hideAnon();
    }

    async refresh() {
        const resource = await this.getResource(this.resourceID);
        this.resourceName.setText(resource.Name.DisplayText);
        if (resource.IsAnonymousAllowed) {
            this.view.showAnon();
        }
        else {
            this.view.hideAnon();
        }
        const resultType = ResourceResultType.values.value(resource.ResultType.Value);
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