import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ResourceResultType } from "../../../Lib/Http/ResourceResultType";
import { ResourceListItemView } from "./ResourceListItemView";
import { AppResource } from "../../../Lib/AppResource";

export class ResourceListItem extends BasicComponent {
    constructor(readonly resource: AppResource, view: ResourceListItemView) {
        super(view);
        new TextComponent(view.resourceName).setText(resource.name.displayText);
        let resultTypeText: string;
        if (
            resource.resultType.equalsAny(ResourceResultType.values.None, ResourceResultType.values.Json)
        ) {
            resultTypeText = '';
        }
        else {
            resultTypeText = resource.resultType.DisplayText;
        }
        new TextComponent(view.resultType).setText(resultTypeText);
    }
}