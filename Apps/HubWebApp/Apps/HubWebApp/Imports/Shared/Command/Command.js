"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Command = void 0;
var tslib_1 = require("tslib");
var AsyncCommand_1 = require("./AsyncCommand");
var Command = /** @class */ (function (_super) {
    tslib_1.__extends(Command, _super);
    function Command(action) {
        return _super.call(this, function (c) {
            return new Promise(function (resolve, reject) {
                try {
                    action(c);
                    resolve({});
                }
                catch (e) {
                    reject(e);
                }
            });
        }) || this;
    }
    return Command;
}(AsyncCommand_1.AsyncCommand));
exports.Command = Command;
//# sourceMappingURL=Command.js.map