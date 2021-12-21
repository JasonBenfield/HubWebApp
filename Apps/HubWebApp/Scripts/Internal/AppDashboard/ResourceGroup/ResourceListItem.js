"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceListItem = void 0;
var ResourceResultType_1 = require("../../../Hub/Api/ResourceResultType");
var ResourceListItem = /** @class */ (function () {
    function ResourceListItem(resource, view) {
        this.resource = resource;
        view.setResourceName(resource.Name);
        var resultType = ResourceResultType_1.ResourceResultType.values.value(resource.ResultType.Value);
        var resultTypeText;
        if (resultType.equalsAny(ResourceResultType_1.ResourceResultType.values.None, ResourceResultType_1.ResourceResultType.values.Json)) {
            resultTypeText = '';
        }
        else {
            resultTypeText = resultType.DisplayText;
        }
        view.setResultType(resultTypeText);
    }
    return ResourceListItem;
}());
exports.ResourceListItem = ResourceListItem;
//# sourceMappingURL=ResourceListItem.js.map