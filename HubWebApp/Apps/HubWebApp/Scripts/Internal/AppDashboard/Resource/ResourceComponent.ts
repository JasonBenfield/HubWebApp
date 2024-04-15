import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ResourceResultType } from '../../../Lib/Http/ResourceResultType';
import { ResourceComponentView } from "./ResourceComponentView";
import { AppResource } from "../../../Lib/AppResource";

export class ResourceComponent {
    private readonly alert: MessageAlert;
    private resourceID: number;
    private readonly resourceName: TextComponent;
    private readonly resultType: TextComponent;

    constructor(
        private readonly hubClient: HubAppClient,
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
        const sourceResource = await this.getResource(this.resourceID);
        const resource = new AppResource(sourceResource);
        this.resourceName.setText(resource.name.displayText);
        if (resource.isAnonymousAllowed) {
            this.view.showAnon();
        }
        else {
            this.view.hideAnon();
        }
        let resultTypeText: string;
        if (
            resource.resultType.equalsAny(ResourceResultType.values.None, ResourceResultType.values.Json)
        ) {
            resultTypeText = '';
        }
        else {
            resultTypeText = resource.resultType.DisplayText;
        }
        this.resultType.setText(resultTypeText);
    }

    private getResource(resourceID: number) {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubClient.Resource.GetResource({
                VersionKey: 'Current',
                ResourceID: resourceID
            })
        );
    }
}