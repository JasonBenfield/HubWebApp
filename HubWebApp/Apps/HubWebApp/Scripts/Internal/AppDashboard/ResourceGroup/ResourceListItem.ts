import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ResourceResultType } from "../../../Hub/Api/ResourceResultType";
import { ResourceListItemView } from "./ResourceListItemView";

export class ResourceListItem {
    constructor(readonly resource: IResourceModel, view: ResourceListItemView) {
        new TextBlock(resource.Name, view.resourceName);
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
        new TextBlock(resultTypeText, view.resultType);
    }
}