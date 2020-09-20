"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.RestrictedGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("../../Hub/AppApiGroup");
var RestrictedGroup = /** @class */ (function (_super) {
    tslib_1.__extends(RestrictedGroup, _super);
    function RestrictedGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'Restricted') || this;
        _this.Index = _this.createView('Index');
        return _this;
    }
    return RestrictedGroup;
}(AppApiGroup_1.AppApiGroup));
exports.RestrictedGroup = RestrictedGroup;
//# sourceMappingURL=RestrictedGroup.js.map