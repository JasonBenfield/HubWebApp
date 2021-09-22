import { Panel } from "./Panel";

export class SingleActivePanel {
    private readonly panels: Panel<any>[] = [];

    add<T extends IComponent>(panelContent: T) {
        let panel = new Panel<T>(panelContent);
        this.panels.push(panel);
        return panel;
    }

    private _previousActive: Panel<any> = null;

    get previousActive() { return this._previousActive; }

    private _currentActive: Panel<any> = null;

    get currentActive() { return this._currentActive; }

    activate(panel: Panel<any>) {
        this._previousActive = this._currentActive;
        for (let p of this.panels) {
            p.deactivate();
        }
        panel.activate();
        this._currentActive = panel;
    }
}