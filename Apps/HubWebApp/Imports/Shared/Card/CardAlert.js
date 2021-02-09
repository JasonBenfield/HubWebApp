"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var MessageAlert_1 = require("../MessageAlert");
var CardBody_1 = require("./CardBody");
var CardAlert = /** @class */ (function (_super) {
    tslib_1.__extends(CardAlert, _super);
    function CardAlert(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.alert = _this.addContent(new MessageAlert_1.MessageAlert());
        _this.hide();
        _this.alert.messageChanged.register(_this.onMessageChanged.bind(_this));
        return _this;
    }
    CardAlert.prototype.onMessageChanged = function (message) {
        if (message) {
            this.show();
        }
        else {
            this.hide();
        }
    };
    return CardAlert;
}(CardBody_1.CardBody));
exports.CardAlert = CardAlert;
//# sourceMappingURL=CardAlert.js.map