"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("../ComponentTemplate");
var ko = require("knockout");
var template = require("./Button.html");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var Events_1 = require("../Events");
var AggregateComponentViewModel_1 = require("./AggregateComponentViewModel");
var ButtonViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ButtonViewModel, _super);
    function ButtonViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('button', template)) || this;
        _this.content = new AggregateComponentViewModel_1.AggregateComponentViewModel();
        _this.type = ko.observable('');
        _this.isEnabled = ko.observable(true);
        _this._clicked = new Events_1.SimpleEvent(_this);
        _this.clicked = _this._clicked.handler();
        return _this;
    }
    ButtonViewModel.prototype.click = function () {
        this._clicked.invoke();
        if (this.type() === "submit") {
            return true;
        }
    };
    return ButtonViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.ButtonViewModel = ButtonViewModel;
//# sourceMappingURL=ButtonViewModel.js.map