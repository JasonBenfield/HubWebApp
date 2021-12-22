import { ResourceResultType } from "../../../Hub/Api/ResourceResultType";
import { ResourceListItemView } from "./ResourceListItemView";

export class ResourceListItem {
    constructor(readonly resource: IResourceModel, view: ResourceListItemView) {
        view.setResourceName(resource.Name);
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
        view.setResultType(resultTypeText);
    }
}