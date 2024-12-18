import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { AppResource } from "../../../Lib/AppResource";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ResourceResultType } from '../../../Lib/Http/ResourceResultType';
import { ResourceComponentView } from "./ResourceComponentView";

export class ResourceComponent {
    private readonly alert: IMessageAlert;
    private resourceID: number;
    private readonly resourceName: TextComponent;
    private readonly resultType: TextComponent;

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: ResourceComponentView
    ) {
        new TextComponent(view.titleHeader).setText('Resource');
        this.alert = new CardAlert(view.alert);
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
            () => this.hubClient.ResourceInquiry.GetResource({
                VersionKey: 'Current',
                ResourceID: resourceID
            })
        );
    }
}