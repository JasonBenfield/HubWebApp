"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.LinkViewModel = void 0;
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("../ComponentTemplate");
var ko = require("knockout");
var template = require("./Link.html");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var Events_1 = require("../Events");
var AggregateComponentViewModel_1 = require("./AggregateComponentViewModel");
var LinkViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(LinkViewModel, _super);
    function LinkViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('link', template)) || this;
        _this.content = new AggregateComponentViewModel_1.AggregateComponentViewModel();
        _this.href = ko.observable('');
        _this.isEnabled = ko.observable(true);
        _this._clicked = new Events_1.SimpleEvent(_this);
        _this.clicked = _this._clicked.handler();
        return _this;
    }
    LinkViewModel.prototype.click = function () {
        this._clicked.invoke();
    };
    return LinkViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.LinkViewModel = LinkViewModel;
//# sourceMappingURL=LinkViewModel.js.map