"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ListCardViewModel = void 0;
var ko = require("knockout");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var Alert_1 = require("XtiShared/Alert");
var template = require("./SelectableListCard.html");
var Events_1 = require("XtiShared/Events");
var ListCardViewModel = /** @class */ (function () {
    function ListCardViewModel() {
        this.componentName = ko.observable('list-card');
        this.alert = new Alert_1.AlertViewModel();
        this.title = ko.observable('');
        this.items = ko.observableArray([]);
        this.hasItems = ko.observable(false);
        this._itemSelected = new Events_1.DefaultEvent(this);
        this.itemSelected = this._itemSelected.handler();
        new ComponentTemplate_1.ComponentTemplate(this.componentName(), template).register();
    }
    ListCardViewModel.prototype.select = function (item) {
        this._itemSelected.invoke(item);
    };
    return ListCardViewModel;
}());
exports.ListCardViewModel = ListCardViewModel;
//# sourceMappingURL=SelectableListCardViewModel.js.map