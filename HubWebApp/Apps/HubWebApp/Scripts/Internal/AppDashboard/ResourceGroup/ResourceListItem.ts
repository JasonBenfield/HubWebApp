import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ResourceResultType } from "../../../Lib/Api/ResourceResultType";
import { ResourceListItemView } from "./ResourceListItemView";

export class ResourceListItem extends BasicComponent {
    constructor(readonly resource: IResourceModel, view: ResourceListItemView) {
        super(view);
        new TextComponent(view.resourceName).setText(resource.Name.DisplayText);
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
        new TextComponent(view.resultType).setText(resultTypeText);
    }
}