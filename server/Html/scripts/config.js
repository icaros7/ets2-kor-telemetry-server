var Funbit;
(function (Funbit) {
    (function (Ets) {
        (function (Telemetry) {
            var serverPort = 25555;

            var Strings = (function () {
                function Strings() {
                }
				Strings.dashboardHtmlLoadFailed = 'dashboard.html 를 스킨으로부터 불러오기 실패 : ';

                Strings.connecting = '연결중...';
                Strings.connected = '연결됨';
                Strings.disconnected = '연결 끊김';
                Strings.enterServerIpMessage = '서버 IP 주소를 입력해주세요. (aa.bb.cc.dd)';
                Strings.incorrectServerIpFormat = '입력하신 서버 IP 주소 형식이 잘못됬습니다.';

                Strings.dayOfTheWeek = [
                    '일요일',
                    '월요일',
                    '화요일',
                    '수요일',
                    '목요일',
                    '금요일',
                    '토요일'
                ];
                Strings.noTimeLeft = '시간 초과';
                Strings.disconnectedFromServer = '서버로부터 연결 끊김';
                Strings.couldNotConnectToServer = '서버에 연결 할 수 없습니다.';
                Strings.connectedAndWaitingForDrive = '연결됬습니다. 주행을 기다리는 중...';
                Strings.connectingToServer = '서버에 연결중...';
                Strings.ownTrailler = '자가 트레일러';
                Strings.Arrived = '목적지에 도착';
                return Strings;
            })();
            Telemetry.Strings = Strings;

            var Configuration = (function () {
                function Configuration() {
                    var _this = this;
                    this.anticacheSeed = Date.now();
                    this.initialized = $.Deferred();
                    this.skins = [];

                    if (!Configuration.isCordovaAvailable()) {
                        this.insomnia = {
                            keepAwake: function () {
                            }
                        };
                        this.prefs = {
                            fetch: function () {
                            },
                            store: function () {
                            }
                        };
                    } else {
                        this.insomnia = plugins.insomnia;
                        this.prefs = plugins.appPreferences;

                        this.insomnia.keepAwake();
                    }

                    var ip = Configuration.getParameter('ip');
                    if (ip) {
                        this.serverIp = ip;
                        this.initialize();
                        return;
                    }
                    this.serverIp = '';
                    if (!Configuration.isCordovaAvailable()) {
                        this.serverIp = window.location.hostname;
                        this.initialize();
                    } else {
                        this.prefs.fetch(function (savedIp) {
                            _this.serverIp = savedIp;
                            _this.initialize();
                        }, function () {
                            _this.initialize();
                        }, 'serverIp');
                    }
                }
                Configuration.getInstance = function () {
                    if (!Configuration.instance) {
                        Configuration.instance = new Configuration();
                    }
                    return Configuration.instance;
                };

                Configuration.getParameter = function (name) {
                    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
                    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)");
                    var results = regex.exec(location.search);
                    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
                };

                Configuration.prototype.getSkinConfiguration = function () {
                    var skinName = Configuration.getParameter('skin');
                    if (skinName) {
                        for (var i = 0; i < this.skins.length; i++) {
                            if (this.skins[i].name == skinName)
                                return this.skins[i];
                        }
                    }
                    return null;
                };

                Configuration.prototype.reload = function (newServerIp, done, fail) {
                    var _this = this;
                    if (typeof done === "undefined") { done = null; }
                    if (typeof fail === "undefined") { fail = null; }
                    if (!newServerIp)
                        return;
                    this.serverIp = newServerIp;
                    this.prefs.store(function () {
                    }, function () {
                    }, 'serverIp', this.serverIp);
                    $.ajax({
                        url: this.getUrlInternal('/config.json?seed=' + this.anticacheSeed++),
                        dataType: 'json',
                        timeout: 3000
                    }).done(function (json) {
                        _this.skins = json.skins;
                        if (done)
                            done();
                    }).fail(function () {
                        _this.skins = [];
                        if (fail)
                            fail();
                    });
                };

                Configuration.isCordovaAvailable = function () {
                    return document.URL.indexOf('http://') === -1 && document.URL.indexOf('https://') === -1;
                };

                Configuration.prototype.getUrlInternal = function (path) {
                    return "http://" + this.serverIp + ":" + serverPort + path;
                };

                Configuration.getUrl = function (path) {
                    return Configuration.getInstance().getUrlInternal(path);
                };

                Configuration.prototype.getSkinResourceUrl = function (skinConfig, name) {
                    return Configuration.getUrl('/skins/' + skinConfig.name + '/' + name + '?seed=' + this.anticacheSeed++);
                };

                Configuration.prototype.initialize = function () {
                    var _this = this;
                    if (!this.serverIp)
                        this.initialized.resolve(this);
                    this.reload(this.serverIp, function () {
                        return _this.initialized.resolve(_this);
                    }, function () {
                        return _this.initialized.resolve(_this);
                    });
                };
                return Configuration;
            })();
            Telemetry.Configuration = Configuration;
        })(Ets.Telemetry || (Ets.Telemetry = {}));
        var Telemetry = Ets.Telemetry;
    })(Funbit.Ets || (Funbit.Ets = {}));
    var Ets = Funbit.Ets;
})(Funbit || (Funbit = {}));
