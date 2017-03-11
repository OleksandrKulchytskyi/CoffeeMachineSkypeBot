"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var index_1 = require("../_services/index");
var HomeComponent = (function () {
    function HomeComponent(route, router, userService) {
        this.route = route;
        this.router = router;
        this.userService = userService;
        this.pending = [];
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }
    HomeComponent.prototype.ngOnInit = function () {
        this.loadPendingUsers();
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/statistics';
    };
    HomeComponent.prototype.approveUser = function (id) {
        var _this = this;
        this.userService.approveUser(id).subscribe(function () { _this.loadPendingUsers(); });
    };
    HomeComponent.prototype.approveAll = function (toApprove) {
        var _this = this;
        var ids = toApprove.map(function (el) { return el.id; });
        this.userService.approveByIds(ids).subscribe(function () { _this.loadPendingUsers(); });
    };
    HomeComponent.prototype.navigateToStatistics = function () {
        this.router.navigate([this.returnUrl]);
    };
    HomeComponent.prototype.loadPendingUsers = function () {
        var _this = this;
        this.userService.getPendingUsers().subscribe(function (users) { _this.pending = users; });
    };
    return HomeComponent;
}());
HomeComponent = __decorate([
    core_1.Component({
        moduleId: module.id,
        templateUrl: 'home.component.html'
    }),
    __metadata("design:paramtypes", [router_1.ActivatedRoute,
        router_1.Router,
        index_1.UserService])
], HomeComponent);
exports.HomeComponent = HomeComponent;
//# sourceMappingURL=home.component.js.map