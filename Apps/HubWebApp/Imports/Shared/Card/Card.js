"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Card = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("../Html/Block");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var CardAlert_1 = require("./CardAlert");
var CardBody_1 = require("./CardBody");
var CardButtonListGroup_1 = require("./CardButtonListGroup");
var CardHeader_1 = require("./CardHeader");
var CardLinkListGroup_1 = require("./CardLinkListGroup");
var CardListGroup_1 = require("./CardListGroup");
var CardTitleHeader_1 = require("./CardTitleHeader");
var Card = /** @class */ (function (_super) {
    tslib_1.__extends(Card, _super);
    function Card(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.addCssName('card');
        return _this;
    }
    Card.prototype.addCardTitleHeader = function (title) {
        return this.addContent(new CardTitleHeader_1.CardTitleHeader(title));
    };
    Card.prototype.addCardHeader = function () {
        return this.addContent(new CardHeader_1.CardHeader());
    };
    Card.prototype.addCardAlert = function () {
        return this.addContent(new CardAlert_1.CardAlert());
    };
    Card.prototype.addCardBody = function () {
        return this.addContent(new CardBody_1.CardBody());
    };
    Card.prototype.addButtonListGroup = function (createItem) {
        if (createItem === void 0) { createItem = null; }
        return this.addContent(new CardButtonListGroup_1.CardButtonListGroup(createItem));
    };
    Card.prototype.addLinkListGroup = function (createItem) {
        if (createItem === void 0) { createItem = null; }
        return this.addContent(new CardLinkListGroup_1.CardLinkListGroup(createItem));
    };
    Card.prototype.addListGroup = function (createItem) {
        if (createItem === void 0) { createItem = null; }
        return this.addContent(new CardListGroup_1.CardListGroup(createItem));
    };
    return Card;
}(Block_1.Block));
exports.Card = Card;
//# sourceMappingURL=Card.js.map