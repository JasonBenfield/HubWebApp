"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ButtonViewModel_1 = require("./ButtonViewModel");
var HtmlContainerComponent_1 = require("./HtmlContainerComponent");
var Button = /** @class */ (function (_super) {
    tslib_1.__extends(Button, _super);
    function Button(vm) {
        if (vm === void 0) { vm = new ButtonViewModel_1.ButtonViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.clicked = _this.vm.clicked;
        _this.isOutline = false;
        vm.type('button');
        _this.addCssName('btn');
        return _this;
    }
    Button.prototype.changeTypeToSubmit = function () {
        this.vm.type('submit');
    };
    Button.prototype.enable = function () { this.vm.isEnabled(true); };
    Button.prototype.disable = function () { this.vm.isEnabled(false); };
    Button.prototype.setContext = function (context) {
        var contextCss = this.getContextCss(context, this.isOutline);
        this.replaceCssName(this.getContextCss(this.context, this.isOutline), contextCss);
        this.context = context;
    };
    Button.prototype.getContextCss = function (context, isOutline) {
        return context ? context.append(isOutline ? 'btn-outline' : 'btn') : '';
    };
    Button.prototype.useOutlineStyle = function () {
        var contextCss = this.getContextCss(this.context, true);
        this.replaceCssName(this.getContextCss(this.context, this.isOutline), contextCss);
        this.isOutline = true;
    };
    return Button;
}(HtmlContainerComponent_1.HtmlContainerComponent));
exports.Button = Button;
//# sourceMappingURL=Button.js.map