"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RequestExpandedListItemViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var template = require("./RequestExpandedListItem.html");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var RequestExpandedListItemViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(RequestExpandedListItemViewModel, _super);
    function RequestExpandedListItemViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('request-expanded-list-item', template)) || this;
        _this.groupName = ko.observable('');
        _this.actionName = ko.observable('');
        _this.userName = ko.observable('');
        _this.timeStarted = ko.observable('');
        return _this;
    }
    return RequestExpandedListItemViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.RequestExpandedListItemViewModel = RequestExpandedListItemViewModel;
//# sourceMappingURL=RequestExpandedListItemViewModel.js.map